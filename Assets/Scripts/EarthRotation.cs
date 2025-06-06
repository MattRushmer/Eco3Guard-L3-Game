using UnityEngine;
public class EarthRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float damping = 2f;
    public Camera mainCamera;
    private bool isRotating = false;
    private Vector3 currentAngularVelocity = Vector3.zero;

    // On update
    void Update()
    {
        Vector3 rotationDirection = Vector3.zero;

        // Rotates the Earth model at a normal speed.
        if (Input.GetKey(KeyCode.D))
        {
            rotationDirection += mainCamera.transform.up;
            isRotating = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotationDirection -= mainCamera.transform.up;
            isRotating = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rotationDirection -= mainCamera.transform.right;
            isRotating = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotationDirection += mainCamera.transform.right;
            isRotating = true;
        }

        // Rotates the Earth model at a faster speed.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rotationSpeed = 100f;
        }
        else
        {
            rotationSpeed = 50f;
        }

        if (rotationDirection != Vector3.zero)
        {
            Quaternion deltaRotation = Quaternion.Euler(rotationDirection * rotationSpeed * Time.deltaTime);
            transform.rotation = deltaRotation * transform.rotation;
            currentAngularVelocity = rotationDirection * rotationSpeed;
        }
        else
        {
            isRotating = false;
        }

        if (!isRotating && currentAngularVelocity != Vector3.zero)
        {
            currentAngularVelocity = Vector3.Lerp(currentAngularVelocity, Vector3.zero, damping * Time.deltaTime);
            Quaternion deltaRotation = Quaternion.Euler(currentAngularVelocity * Time.deltaTime);
            transform.rotation = deltaRotation * transform.rotation;
        }
    }
}
