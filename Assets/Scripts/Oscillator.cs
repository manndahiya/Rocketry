using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    
    void Start()
    {
        startPos = transform.position;
    }

    
    void Update()
    {
        if (period <= Mathf.Epsilon) // to fix NaN error
        {
            return;
        }

        float cycles = Time.time / period;  // continually grows over time
        const float tau = Mathf.PI * 2;  //6.28
        float rawSinWave = Mathf.Sin(cycles * tau); //parameter is radians [-1,1]

        movementFactor = (rawSinWave + 1f) / 2f; // [0,1]

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;

        
    }
}
