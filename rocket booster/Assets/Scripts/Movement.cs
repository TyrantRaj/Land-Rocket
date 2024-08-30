using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] float thrustAmount = 1f;
    [SerializeField] float rotateAmount = 1f;

    private void Start()
    {
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
        }
        
    }

    private void ProcessRotation()
    {
        body.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateAmount);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateAmount);
        }

        body.freezeRotation = false;
    }
}
