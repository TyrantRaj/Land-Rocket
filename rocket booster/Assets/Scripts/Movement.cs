using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;

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
    float num;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();
        moveAction = input.actions.FindAction("Move");
        body = GetComponent<Rigidbody>();

    }

    // Update is caled once per frame
    void FixedUpdate()
    {
        Debug.Log(moveAction.ReadValue<Vector2>());

        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || moveAction.ReadValue<Vector2>().y == 1)
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

        if (Input.GetKey(KeyCode.A) || moveAction.ReadValue<Vector2>().x == -1)
        {
            transform.Rotate(Vector3.forward * rotateAmount);

            if (!rightps.isPlaying)
            {
                rightps.Play();
                middleps1.Play();
                middleps2.Play();
            }

        }
        else if (Input.GetKey(KeyCode.D) || moveAction.ReadValue<Vector2>().x == 1)
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
