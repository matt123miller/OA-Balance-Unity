using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ParticipantController : MonoBehaviour
{

    public delegate void CollisionDelegate();
    public delegate void ReturnHomeDelegate();
    public delegate void RestTimerFinishedDelegate();
    public delegate void LeaveHomeDelegate();
    public event CollisionDelegate targetCollisionEvent;
    public event RestTimerFinishedDelegate restFinishedEvent;
    public event ReturnHomeDelegate returnHomeEvent;
    public event LeaveHomeDelegate leaveHomeEvent;

    public bool experimentBegun = false;
    public bool targetReached = false;
    public bool timerOn = true;
    public float timeBetweenMovements = 5f;
    public float timer = 0f;


    // Use this for initialization
    void Start()
    {

    }

    private void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;

            if (timer > timeBetweenMovements)
            {
                timerOn = false;
                timer = 0f;
                experimentBegun = true;

                if (restFinishedEvent != null)
                {
                    restFinishedEvent();
                }
                print("rest finished!");
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            //print("Hit target");
            if (targetCollisionEvent != null)
            {
                targetCollisionEvent();
            }
        }
        else if (other.CompareTag("HomeArea"))
        {
            //print("Hit home");
            if (returnHomeEvent != null)
            {
                if (experimentBegun)
                {
                    timerOn = true;
                    experimentBegun = false;
                    returnHomeEvent();
                }
            }
            //print("Now move to the next target when ready");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HomeArea"))
        {
            if (leaveHomeEvent != null)
            {
                if (timerOn == false)
                {
                    leaveHomeEvent();
                }
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    public void MoveBy(Vector3 velocity)
    {
        transform.position += velocity;
    }

}
