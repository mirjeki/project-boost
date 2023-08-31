using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] AudioClip thrustSFX;
    [SerializeField] float volume = 0.6f;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody myRigidbody;
    Transform myTransform;
    AudioPlayer audioPlayer;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartThrusting()
    {
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
        if (!audioPlayer.IsCurrentlyPlaying(StaticReferences.SFXChannel))
        {
            audioPlayer.PlayClipOnce(thrustSFX, volume, StaticReferences.SFXChannel);
        }
        myRigidbody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
    }

    private void StopThrusting()
    {
        thrustParticles.Stop();
        audioPlayer.StopAudio(StaticReferences.SFXChannel);
    }

    public void DisableMovement()
    {
        this.enabled = false;
        if (audioPlayer.IsCurrentlyPlaying(StaticReferences.SFXChannel))
        {
            audioPlayer.StopAudio(StaticReferences.SFXChannel);
        }
    }

    private void RotateRight()
    {
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
        rightThrustParticles.Stop();
        CalculateRotation(rotateSpeed);
    }

    private void RotateLeft()
    {
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
        leftThrustParticles.Stop();
        CalculateRotation(-rotateSpeed);
    }

    private void CalculateRotation(float rotateSpeed)
    {
        myRigidbody.freezeRotation = true;
        myTransform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }

    private void StopRotation()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }
}
