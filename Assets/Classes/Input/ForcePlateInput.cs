using UnityEngine;
using System.Collections;

public class ForcePlateInput : MonoBehaviour {

    private VisualiseController controller;

    void Awake()
    {
        controller = GetComponent<VisualiseController>();
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
