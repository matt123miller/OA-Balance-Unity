using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class GlobalDataManager : MonoBehaviour
{
    private static GlobalDataManager _instance;
    [Range(1, 10)]
    public int columns, rows;
    public int[,] dataGrid;
    public string[] columnHeadings;

    public ParticipantController participant;
    public bool recording = false;
    public Dictionary<string, List<Vector2>> allRecordingsDict;
    public List<Vector2> currentRecording;

    public string filePath;
    public string csvTitle;



    public static GlobalDataManager Instance
    {
        get { return _instance ?? (_instance = new GlobalDataManager()); }
    }

    // Use this for initialization
    void Start()
    {
        currentRecording = new List<Vector2>();
        allRecordingsDict = new Dictionary<string, List<Vector2>>();

        participant = FindObjectOfType<ParticipantController>();
        participant.returnHomeEvent += ToggleRecording;
        participant.leaveHomeEvent += ToggleRecording;

        SetupFilePath();

        // Remove when the proper data recording works.
        PopulateDataGrid();
    }



    private void ToggleRecording()
    {
        if (recording)
        {
            // add to the allRecordings dictionary
            string key = allRecordingsDict.Keys.Count.ToString();
            allRecordingsDict.Add(key, currentRecording);
            // Replace current recording with a new one
            currentRecording = new List<Vector2>();

            // Whats in my dictionary?
            print("There are " + allRecordingsDict.Keys.Count + " Keys in allRecordingsDict");
            print("The most recent value added contains " + allRecordingsDict["0"].Count + " items");
        }


        recording = !recording;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (recording)
        {
            currentRecording.Add(participant.transform.position);
        }
    }

    public void ButtonClick()
    {
        SaveOutputGrid(dataGrid);
    }

    private void PopulateDataGrid()
    {
        dataGrid = new int[columns, rows]; // array initiliase counts from 1
        string strOutput = "";

        // populate the array with some junk data.
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                dataGrid[x, y] = Random.Range(0, 256);

                //Remove later
                //strOutput = y + " " + x;
                //print(strOutput);
            }
        }
        // Bit of validation
        //print("grid size" + (dataGrid.Length));
    }

    public void SaveOutputGrid<T>(T[,] grid)
    {
        StringBuilder textOutput = new StringBuilder();

        // Create column headings
        for (int i = 0; i < columnHeadings.Length; i++)
        {
            textOutput.Append(columnHeadings[i]);
            if (i != columnHeadings.Length)
            {
                textOutput.Append(",");
            }
        }
        textOutput.Append(Environment.NewLine);

        // Add the data collected to the rows and columns
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                textOutput.Append(grid[column, row].ToString());

                // This will add a comma unless the end of the row is reached.
                if (column != columns)
                    textOutput.Append(";");
            }
            textOutput.Append(Environment.NewLine);
        }

        System.IO.File.AppendAllText(filePath, textOutput.ToString());
    }

    private void SetupFilePath()
    {
        DirectoryInfo dir = new DirectoryInfo(GetPath() + "/CSV");
        FileInfo[] files = dir.GetFiles();

        int csvTotal = 1;

        foreach (FileInfo file in files)
            if (file.Extension == ".csv")
                csvTotal += 1;

        if (csvTitle == "")
            csvTitle = "Participant" + csvTotal + ".csv";
        else // save the file as whatever the user inputs.
            csvTitle += ".csv";

        filePath = GetPath(csvTitle);
    }

    void OnValidate()
    {
        // it would be nice to have the columnHeadings array resize when you change the columns number, 
        // this probably requieres a List as well

        //var temp = columnHeadings.Take(columnHeadings.Count);
        //columnHeadings = new list;
        // column headings fill with temp, take into account pos or neg change;
    }

    public string GetPath(string csvTitle)
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + csvTitle;
#elif UNITY_ANDROID
        return Application.persistentDataPath+csvTitle;
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+csvTitle;
#else
        return Application.dataPath + "/" + csvTitle;
#endif

        // Will I have to manually add windows/mac support here?
    }

    public string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath;
#elif UNITY_ANDROID
        return Application.persistentDataPath;
#elif UNITY_IPHONE
        return Application.persistentDataPath;
#else
        return Application.dataPath;
#endif

        // Will I have to manually add windows/mac support here?
    }

}

