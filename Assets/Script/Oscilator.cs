using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    float offsetFactor;
    [SerializeField] float period;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon ){ return; }
        float cycles = Time.time / period; // Continue growing

        const float tau = Mathf.PI * 2; //Constants value is 6.28
        float rawSinWave = Mathf.Sin(tau * cycles); // The value is -1 to 1

        offsetFactor = (rawSinWave +1f) / 2f;

        Vector3 offset = movementVector * offsetFactor;
        transform.position = startPosition + offset;
    }
}
