using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticipantController))]
public class KeyboardInput : MonoBehaviour
{
    private ParticipantController controller;
    public float moveMultiplier;

	void Awake ()
	{
	    controller = GetComponent<ParticipantController>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	    movement = movement.normalized * moveMultiplier;
        controller.MoveBy(movement);
	}
}
