using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour
{
    private VisualiseController controller;
    public float moveMultiplier;

	void Awake ()
	{
	    controller = GetComponent<VisualiseController>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	    movement = movement.normalized * moveMultiplier;
        controller.MoveBy(movement);
	}
}
