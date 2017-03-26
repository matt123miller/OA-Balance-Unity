using UnityEngine;
using System.Collections;

public class ForcePlateInput : MonoBehaviour
{

    private ParticipantController controller;

    public Vector2[] inputVectors;
    public int currentVector = 0;

    void Awake()
    {
        controller = GetComponent<ParticipantController>();
    }

    void Start()
    {
        // Try to normalise all the vectors to be centred ish
        var firstVector = inputVectors[100];

        for (int i = 0; i < inputVectors.Length; i++)
        {
            inputVectors[i] -= firstVector;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentVector < inputVectors.Length)
        {
            var cop = inputVectors[currentVector];
            currentVector += 1;
            controller.MoveTo(cop);
        }
        else
        {
            // End the input / simulation?
            Destroy(this);
        }
    }
}
