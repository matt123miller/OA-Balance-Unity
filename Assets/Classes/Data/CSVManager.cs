using UnityEngine;
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

    void Start()
    {
        filePath = SetupFilePath();
        Debug.Log(filePath);
    }

    // Generic T MUST implement .ToString() or everything breaks
    public void SaveOutputGrid<T>(Dictionary<string, List<T>> data)
    {
        StringBuilder textOutput = new StringBuilder();
        
        foreach (string key in data.Keys)
        {
            var list = data[key];
            textOutput.Append(key);

            for (int i = 0; i < list.Count; i++)
            {
                textOutput.Append(",");
                // Hopefully no array overrun
                var vector = escapeQuotes ? "\"" + list[i].ToString() + "\"" : list[i].ToString();
                textOutput.Append(vector);
            }

            textOutput.Append(Environment.NewLine);
        }

        System.IO.File.AppendAllText(filePath, textOutput.ToString());
    }

    private string SetupFilePath()
    {
        // All this just decides what to name the file
        DirectoryInfo dir = new DirectoryInfo(GetPath() + "/CSV/");
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
#else
        return Application.dataPath;
#endif

        // Will I have to manually add windows/mac support here?
    }
}