using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;

public class CSVManager : MonoBehaviour
{

    public string filePath;
    public string csvTitle;
    public bool escapeQuotes = false;
    public bool fullySplitVector = false;
    public Text textField;

    void Start()
    {
        filePath = SetupFilePath();
        Debug.Log(filePath);
        textField.text = filePath;
    }

    // Generic T MUST implement .ToString() or everything breaks
    public void SaveOutputGrid(Dictionary<string, List<Vector2>> data)
    {
        StringBuilder textOutput = new StringBuilder();
        string vector;

        foreach (string key in data.Keys)
        {
            var list = data[key];
            textOutput.Append(key);
            //print(key);

            for (int i = 0; i < list.Count; i++)
            {
                textOutput.Append(",");
                // Hopefully no array overrun
                //if (fullySplitVector)
                vector = (double)list[i].x + "," + (double)list[i].y;

                //else if (!fullySplitVector && escapeQuotes)
                //vector = "\"" + list[i].ToString() + "\"";

                //else
                //    vector = list[i].ToString();


                textOutput.Append(vector);
            }

            textOutput.Append(Environment.NewLine);
        }

        System.IO.File.AppendAllText(filePath, textOutput.ToString());
    }

    private string SetupFilePath()
    {
        // All this just decides what to name the file

        DirectoryInfo dir;
#if UNITY_EDITOR
        dir = new DirectoryInfo(GetPath() + "/CSV/");
#else
        // This is the final build option
        dir = new DirectoryInfo(GetPath());
#endif
        FileInfo[] files = dir.GetFiles();
        int csvTotal = 1;

        foreach (FileInfo file in files)
            if (file.Extension == ".csv")
                csvTotal += 1;

        if (csvTitle == "")
            csvTitle = "Participant" + csvTotal + ".csv";
        else // save the file as whatever the user inputs.
            csvTitle += ".csv";

        // Then this will actually create the path using the gile name
        return dir.ToString() + csvTitle;
    }


    // Will I have to manually add windows/mac support here?
    public string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath;
#elif UNITY_ANDROID
        return Application.persistentDataPath;
#elif UNITY_IPHONE
        return Application.persistentDataPath;
#endif
        return Application.dataPath;
        // Will I have to manually add windows/mac support here?
    }
}