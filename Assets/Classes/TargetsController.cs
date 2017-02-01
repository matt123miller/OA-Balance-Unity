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
        SubscribeEvents();

        target = GameObject.FindWithTag("Target").transform;
        target.position = targets[positionIndex].position;

        target.gameObject.SetActive(false);
    }

    private void SubscribeEvents()
    {
        participant.targetCollisionEvent += DisplayTarget;
        participant.returnHomeEvent += ChangeTarget; // Change target must go first on the returnHomeEvent.
        participant.restFinishedEvent += DisplayTarget;
    }

    // Update is called once per frame
    private void UnsubscribeEvents()
    {
        participant.targetCollisionEvent -= DisplayTarget;
        participant.returnHomeEvent -= ChangeTarget; 
        participant.restFinishedEvent -= DisplayTarget;

    }

    // Should be called whenever the participant reaches the target
    private void ChangeTarget()
    {
        positionIndex++;

        if (positionIndex < targets.Length)
        {
            target.position = targets[positionIndex].position;
        }
        else
        {
            //target.GetComponent<Collider>().enabled = false;
            GlobalDataManager.Instance.FinishTest();
            UnsubscribeEvents();
        }





        //{
        //    target.gameObject.SetActive(false);
        //}
        //else
        //{
        //}

    }

    //Should be called whenever participant reaches the target or rest
    private void DisplayTarget()
    {
        if (positionIndex < targets.Length)
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
}
