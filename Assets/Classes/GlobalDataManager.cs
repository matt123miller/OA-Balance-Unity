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
    private CSVManager csv;
    public ParticipantController participant;

    public bool recording = false;
    public Dictionary<string, List<Vector2>> allRecordingsDict;
    public string[] columnHeadings;
    public List<Vector2> currentRecording;


    public static GlobalDataManager Instance
    {
        get { return _instance ?? (_instance = new GlobalDataManager()); }
    }

    private void Awake()
    {
        _instance = this;

        csv = new CSVManager();
    }


    // Use this for initialization
    void Start()
    {
        currentRecording = new List<Vector2>();
        allRecordingsDict = new Dictionary<string, List<Vector2>>();

        participant = FindObjectOfType<ParticipantController>();
        participant.returnHomeEvent += ToggleRecording;
        participant.leaveHomeEvent += ToggleRecording;

    }



    private void ToggleRecording()
    {
        if (recording)
        {
            // add to the allRecordings dictionary
            string key = columnHeadings[allRecordingsDict.Keys.Count];
            allRecordingsDict.Add(key, currentRecording);

            // Whats in my dictionary?
            print("Added " + key + " to the dictionary");
            print("There are " + allRecordingsDict.Keys.Count + " Keys in allRecordingsDict");
            print("The most recent value added contains " + currentRecording.Count + " items");

            // Replace current recording with a new one
            currentRecording = new List<Vector2>();
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
        csv.SaveOutputGrid(allRecordingsDict);
    }


    void OnValidate()
    {
        // it would be nice to have the columnHeadings array resize when you change the columns number, 
        // this probably requieres a List as well

        //var temp = columnHeadings.Take(columnHeadings.Count);
        //columnHeadings = new list;
        // column headings fill with temp, take into account pos or neg change;
    }


}

