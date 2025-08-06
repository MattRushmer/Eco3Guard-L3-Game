
using UnityEngine;

public class MoonRotation : MonoBehaviour
{
    public float orbitSpeed = 50f; // Moon rotation speed
    public float rotationSpeed = 5f; // Moon rotation speed

    void Update()
    {
        transform.Rotate(0, orbitSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime);
    }
}
