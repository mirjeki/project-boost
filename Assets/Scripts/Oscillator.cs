using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    [SerializeField] float delay = 0f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        //Mathf.Epsilon
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        if (Time.time > delay)
        {
            float cycles = Time.time / period; // continually growing over time

            const float tau = Mathf.PI * 2; // 6.283...
            float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

            movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

            Vector3 offset = movementVector * movementFactor;

            transform.position = startingPosition + offset;
        }
    }
}
