using UnityEngine;
using System.Collections;

public class VisualiseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
