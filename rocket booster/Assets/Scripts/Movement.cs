using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    PlayerInput input;
    InputAction moveRight;
    InputAction moveLeft;
    InputAction Throttle;

    [SerializeField]GameObject Planet;
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
        moveRight = input.actions.FindAction("right");
        moveLeft = input.actions.FindAction("left");
        Throttle = input.actions.FindAction("throttle");
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
        // Apply upward force when space or throttle is pressed
        if (Input.GetKey(KeyCode.Space) || Throttle.IsInProgress())
        {
            // Add upward force relative to the rocket's orientation
            body.AddRelativeForce(Vector3.up * thrustAmount);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainengine);
            }

            if (!mainThrottlePs.isPlaying)
            {
                mainThrottlePs.Play();
            }

            // Check if rotation input is also being pressed
            if (Input.GetKey(KeyCode.A) || moveLeft.IsInProgress())
            {
                // Rotate the rocket and the planet when throttle + left input is pressed
                RotatePlanetAndRocket(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.D) || moveRight.IsInProgress())
            {
                // Rotate the rocket and the planet when throttle + right input is pressed
                RotatePlanetAndRocket(-Vector3.forward);
            }
        }
        else
        {
            // Stop audio and particles when thrust is not applied
            audioSource.Stop();
            mainThrottlePs.Stop();
        }
    }

    private void RotatePlanetAndRocket(Vector3 rotationDirection)
    {
        // Rotate the rocket in place around its local axis
        transform.Rotate(rotationDirection * rotateAmount * Time.deltaTime);

        // Rotate the planet along the Y-axis
        Planet.transform.Rotate(Vector3.up * rotateAmount * Time.deltaTime);
    }

    private void ProcessRotation()
    {
        // Disable automatic rotation from the physics system
        body.freezeRotation = true;

        // Only rotate when thrust is being applied
        if (!(Input.GetKey(KeyCode.Space) || Throttle.IsInProgress()))
        {
            // If no thrust, handle normal rotation without planet rotation
            if (Input.GetKey(KeyCode.A) || moveLeft.IsInProgress())
            {
                // Rotate the rocket left without affecting the planet
                transform.Rotate(Vector3.forward * rotateAmount * Time.deltaTime);

                if (!rightps.isPlaying)
                {
                    rightps.Play();
                    middleps1.Play();
                    middleps2.Play();
                }
            }
            else if (Input.GetKey(KeyCode.D) || moveRight.IsInProgress())
            {
                // Rotate the rocket right without affecting the planet
                transform.Rotate(-Vector3.forward * rotateAmount * Time.deltaTime);

                if (!leftps.isPlaying)
                {
                    leftps.Play();
                    middleps1.Play();
                    middleps2.Play();
                }
            }
            else
            {
                // Stop the particle effects if no rotation inputs are detected
                rightps.Stop();
                leftps.Stop();
                middleps1.Stop();
                middleps2.Stop();
            }
        }

        // Re-enable automatic rotation
        body.freezeRotation = false;
    }





}
