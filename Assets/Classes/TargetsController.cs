using System;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class TargetsController : MonoBehaviour
{
    public ParticipantController participant;
    public Transform[] targets;
    public int positionIndex = 0;

    public Transform target;




    // Use this for initialization
    private void Start()
    {
        // Subscribe to the collision event
        participant = FindObjectOfType<ParticipantController>();
        participant.targetCollisionEvent += ChangeTarget;
        participant.targetCollisionEvent += DisplayTarget;
        participant.returnHomeEvent += DisplayTarget;

        target = GameObject.FindWithTag("Target").transform;
        target.position = targets[positionIndex].position;


    }

    // Update is called once per frame
    private void Update()
    {


    }

    // Should be called whenever the participant reaches the target
    private void ChangeTarget()
    {
        positionIndex = positionIndex + 1 >= targets.Length ? 0 : positionIndex + 1;

        target.position = targets[positionIndex].position;
    }

    //Should be called whenever participant reaches the target or rest
    private void DisplayTarget()
    {
        if (target.gameObject.activeInHierarchy)
        {
            target.gameObject.SetActive(false);
        }
        else
        {
            target.gameObject.SetActive(true);
        }
    }
}
