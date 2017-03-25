using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Random = UnityEngine.Random;

public class CSVManager : MonoBehaviour
{
    public enum CSVState
    {
        read = 0,
        write = 1
    }

    [SerializeField]
    public CSVState state = 0;
    public string filePath;
    public string csvTitle;
    public Text textField;

    void Awake()
    {
        filePath = SetupFilePath();
        Debug.Log(filePath);
        if (state == CSVState.read)
        {
            // Load the file in and send it to the right place.
            StreamReader sr = new StreamReader(filePath);
            var lines = new List<string[]>();
            int Row = 0;
            print(sr.ReadToEnd());
            // Y U NO READ?!?!
            //while (!sr.EndOfStream)
            //{
            //    string[] strInfo = sr.ReadLine().Split(',');
            //    Vector2 v2 = new Vector2();
            //    print(strInfo);
            //    lines.Add(strInfo);
            //    Row++;
            //    Console.WriteLine(Row);
            //}

            //var data = lines.ToArray();
            //print(data);
            // Send it to the right place
        }
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
                vector = (double)list[i].x + "," + (double)list[i].y;

                textOutput.Append(vector);
            }

            textOutput.Append(Environment.NewLine);
        }

        System.IO.File.AppendAllText(filePath, textOutput.ToString());
    }



    private string SetupFilePath()
    {
        DirectoryInfo dir = new DirectoryInfo(GetPath() + "/CSV/"); 

        switch (state)
        {
            case CSVState.read:
                dir = new DirectoryInfo(GetPath() + "/CSV/Read/");
                var readFiles = ShuffleArray(dir.GetFiles());
                // choose a random file 
                string path = "";
                for (int i = 0; i < readFiles.Length; i++)
                {
                    var file = readFiles[i];
                    if (!file.Extension.Contains("meta"))
                    {
                        path = file.Directory + "\\" + file.Name;
                    }

                }
                print(path);
                // I have a path so now read the fi

                break;
            case CSVState.write:
                // All this just decides what to name the file

#if UNITY_EDITOR

#else
        // This is the final build option
        dir = new DirectoryInfo(GetPath());
#endif

                FileInfo[] writeFiles = dir.GetFiles();
                int csvTotal = 1;

                foreach (FileInfo file in writeFiles)
                    if (file.Extension == ".csv")
                        csvTotal += 1;

                if (csvTitle == "")
                    csvTitle = "Participant" + csvTotal + ".csv";
                else // save the file as whatever the user inputs.
                    csvTitle += ".csv";

                // Then this will actually create the path using the gile name
                //return dir.ToString() + csvTitle;
                break;
            default:
                break;
        }

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

    T[] ShuffleArray<T>(T[] array)
    {
        var r = new System.Random();
        for (int i = array.Length; i > 0; i--)
        {
            int j = r.Next(i);
            T k = array[j];
            array[j] = array[i - 1];
            array[i - 1] = k;
        }
        return array;
    }
}