using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float Thrustspeed;
    [SerializeField] float Rotatespeed;

    [SerializeField] AudioClip rocketEngine;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    [SerializeField] ParticleSystem mainThrust;

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
        // Main move to go up
        ProcessThrust();
        // Rotating the rocket
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            // Give force
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }


    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * Thrustspeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketEngine);
        }
        if (!mainThrust.isPlaying)
        {
            mainThrust.Play();
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainThrust.Stop();
    }

    void ProcessRotate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            // Rotating to the left
            StartLeftThrust();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            // Rotating to the right
            StartRightThrust();
        }
        else
        {
            StopLeftRightThrust();
        }
    }


    private void StartLeftThrust()
    {
        ApplyRotation(-Rotatespeed);
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
        }
    }

    private void StartRightThrust()
    {
        ApplyRotation(Rotatespeed);
        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();
        }
    }

    private void StopLeftRightThrust()
    {
        leftThrust.Stop();
        rightThrust.Stop();
    }

    void ApplyRotation(float Rotatespeed)
    {
        // freeze the rotation from external factor
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Rotatespeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
