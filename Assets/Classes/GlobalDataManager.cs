using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Random = UnityEngine.Random;

public class GlobalDataManager : MonoBehaviour
{
    private static GlobalDataManager _instance;
    [Range(1, 5)] public int width, height;
    public int[,] dataGrid;
    public string filePath;
    public string csvTitle;



    public static GlobalDataManager Instance
    {
        get { return _instance ?? (_instance = new GlobalDataManager()); }
    }

    // Use this for initialization
    void Start()
    {
        dataGrid = new int[width,height];
        SetupFilePath();

        int i = 0;
        // populate my data.
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                dataGrid[x, y] = Random.Range(0, 256);
                i++;
                print(i);
            }
        }
        print("grid size" + (dataGrid.Length));

        //SaveOutputGrid(dataGrid);
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void buttonClick() { SaveOutputGrid(dataGrid); }

    private void SetupFilePath()
    {
        DirectoryInfo dir = new DirectoryInfo(GetPath() + "/CSV");
        List<FileInfo> files = dir.GetFiles().ToList();

        int csvTotal = 1;

        foreach (FileInfo file in files)
            if (file.Extension == ".csv")
                csvTotal += 1;

        if (csvTitle == "")
            csvTitle = "Participant" + csvTotal + ".csv";

        filePath = GetPath(csvTitle);
    }


    public void SaveOutputGrid<T>(T[,] grid)
    {
        StringBuilder textOutput = new StringBuilder();

        int upperBound = grid.GetUpperBound(0) - 1;
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {
                
                textOutput.AppendLine(grid[x, y].ToString());
                if (x != upperBound)
                    textOutput.Append(",");
            }
            textOutput.Append(Environment.NewLine);
        }

        System.IO.File.WriteAllText(filePath, textOutput.ToString());
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

