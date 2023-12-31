using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    //Allow for slider tool in the inspector. [SerializeField] [Range(0,1)]
     float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // was getting a NaN error because we could not divide by zero therefore I had to change to Mathf.Epsilon.
        // if period is <= a tiny number rather than saying "== 0f"
        if (period <= Mathf.Epsilon) { return; }
        //Measure time. 2 seconds / 2 seconds would equal 1.
        float cycles = Time.time / period; // continually growing over time.
        //need tau as a value. Tau is the complete radius of a circle or cycle.
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner.

        //Update position every frame
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
