using UnityEngine;
using System.Collections;

public class HomeController : MonoBehaviour
{

    public new Collider collider;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }
	// Use this for initialization
	void Start () {

        var participant = FindObjectOfType<ParticipantController>();
	    participant.leaveHomeEvent += ToggleHomeCollision;
        participant.targetCollisionEvent += ToggleHomeCollision;
    }

    // Update is called once per frame
    void Update () {
	
	}

    private void ToggleHomeCollision()
    {
        //collider.enabled = !collider.enabled;
    }
}
