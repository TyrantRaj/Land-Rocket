using UnityEngine;

public class CameraRotateWithRocket : MonoBehaviour
{
    public Transform rocket; // Reference to the rocket's transform

    void LateUpdate()
    {
        // Keep the camera's position fixed but match its rotation with the rocket's rotation
        transform.rotation = rocket.rotation;
    }
}
