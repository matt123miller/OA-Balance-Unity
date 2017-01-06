using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ParticipantController : MonoBehaviour
{

    public delegate void CollisionDelegate();
    public delegate void ReturnHomeDelegate();
    public delegate void LeaveHomeDelegate();
    public event CollisionDelegate targetCollisionEvent;
    public event ReturnHomeDelegate returnHomeEvent;
    public event LeaveHomeDelegate leaveHomeEvent;

    public bool experimentBegun = false;


	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            print("Hit target");
            if (targetCollisionEvent != null)
            {
                targetCollisionEvent();
            }
        }
        else if (other.CompareTag("HomeArea"))
        {
            print("Hit home");
            if (returnHomeEvent != null)
            {
                if (experimentBegun)
                {
                    returnHomeEvent();
                }
            }
            print("Now move to the next target when ready");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HomeArea"))
        {
            if (leaveHomeEvent != null)
            {
                experimentBegun = true;
                leaveHomeEvent();
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
