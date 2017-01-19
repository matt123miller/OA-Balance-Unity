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
                textOutput.Append(list[i].ToString());
            }

            textOutput.Append(Environment.NewLine);
        }

        // Do I need this really? I'll experiment without, it's really just to make the data more human readable in Excel

        // Create column headings
        //for (int i = 0; i < columnHeadings.Length; i++)
        //{
        //    textOutput.Append(columnHeadings[i]);
        //    if (i != columnHeadings.Length)
        //    {
        //        textOutput.Append(", ,"); // This might make everything bad
        //    }
        //}
        //textOutput.Append(Environment.NewLine);

        //// Add the data collected to the rows and columns
        //for (int row = 0; row < rows; row++)
        //{
        //    for (int column = 0; column < columns; column++)
        //    {
        //        //textOutput.Append(data[column, row].ToString());

        //        // Add a comma unless the end of the row is reached.
        //        if (column != columns)
        //            textOutput.Append(",");
        //    }
        //    textOutput.Append(Environment.NewLine);
        //}
        var path = SetupFilePath();
        print(path);
        System.IO.File.AppendAllText(path, textOutput.ToString());
    }

    private string SetupFilePath()
    {
        // All this just decides what to name the file
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

        // Then this will actually create the path using the gile name
        return GetPath() + "/CSV/" + csvTitle;
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
