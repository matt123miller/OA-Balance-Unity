using UnityEngine;
using System.Collections;

public class KeyboardInput : BaseInput {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	    float x = Input.GetAxisRaw("Horizontal");
	    float y = Input.GetAxisRaw("Vertical");
        MoveTo(new Vector2(x, y));

	}
}
