using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] ParticleSystem mainThrottlePs;
    [SerializeField] ParticleSystem rightps;
    [SerializeField] ParticleSystem leftps;
    [SerializeField] ParticleSystem middleps1;
    [SerializeField] ParticleSystem middleps2;

    Rigidbody body;
    [SerializeField] float thrustAmount = 1f;
    [SerializeField] float rotateAmount = 1f;
    AudioSource audioSource;
    [SerializeField] AudioClip mainengine;

    bool isAlive;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
    }

    // Update is caled once per frame
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            body.AddRelativeForce(Vector3.up * thrustAmount);

            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainengine);
                
            }
            if (!mainThrottlePs.isPlaying) {
                mainThrottlePs.Play();
            }
            
        }
        else
        {
            audioSource.Stop();
            mainThrottlePs.Stop();
        }
        
    }


    private void ProcessRotation()
    {
        body.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateAmount);

            if (!rightps.isPlaying)
            {
                rightps.Play();
                middleps1.Play();
                middleps2.Play();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateAmount);

            if (!leftps.isPlaying)
            {
                leftps.Play();
                middleps1.Play();
                middleps2.Play();
            }
        }
        else { 
            rightps.Stop();
            leftps.Stop();
            middleps1.Stop();
            middleps2.Stop();
        }

        body.freezeRotation = false;
    }
}
