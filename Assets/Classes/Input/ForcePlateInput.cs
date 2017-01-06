using UnityEngine;
using System.Collections;

public class ForcePlateInput : MonoBehaviour {

    private ParticipantController controller;

    void Awake()
    {
        controller = GetComponent<ParticipantController>();
    }

    // Update is called once per frame
    void Update () {
	    
        // Get data from the walking platform
        
        // parse it somehow

        // make a Vector2 from it
        var cop = new Vector2(0,0);
        controller.MoveTo(cop);
	}
}
