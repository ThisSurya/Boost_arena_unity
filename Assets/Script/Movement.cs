using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float Thrustspeed;
    [SerializeField] float Rotatespeed;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    
    void Start()
    {
         rb = GetComponent<Rigidbody>();   
         audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Thrustspeed * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-Rotatespeed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Rotatespeed);
        }
    }

    void ApplyRotation(float Rotatespeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Rotatespeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
