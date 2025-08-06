using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;
public class EarthRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float damping = 2f;
    public Camera mainCamera;
    private bool isRotating = false;
    private Vector3 currentAngularVelocity = Vector3.zero;

    public float scrollRotationSpeed;
    private RaycastHit raycastHit;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

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


        //Rotates the Earth When you drag
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            if(Vector3.Dot(transform.forward, Vector3.up) >= 0)
            {
                transform.Rotate(transform.forward, -Vector3.Dot(mPosDelta, Camera.main.transform.right * Time.deltaTime * scrollRotationSpeed), Space.World);
            }
            else
            {
                transform.Rotate(transform.forward, Vector3.Dot(mPosDelta, Camera.main.transform.right * Time.deltaTime * scrollRotationSpeed), Space.World);
            }

            
            transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up * Time.deltaTime * scrollRotationSpeed), Space.World);
        }

        mPrevPos = Input.mousePosition;


    }

}
