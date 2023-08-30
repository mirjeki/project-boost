using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] float rotateSpeed = 5f;

    Rigidbody myRigidbody;
    Transform myTransform;
    AudioSource myAudioSource;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.Play();
            }
            myRigidbody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        }
        else
        {
            myAudioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            CalculateRotation(-rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CalculateRotation(rotateSpeed);
        }
    }

    private void CalculateRotation(float rotateSpeed)
    {
        myRigidbody.freezeRotation = true;
        myTransform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}
