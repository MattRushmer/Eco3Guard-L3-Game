using UnityEngine;

public class EarthZoom : MonoBehaviour
{

    private Camera camera;
    private float zoomTarget;

    [SerializeField]
    private float multiplier = 2f, minZoom = 20f, maxZoom = 80f, smoothTime = .1f;
    private float velocity = 0f;

    private void Start()
    {
        camera = GetComponent<Camera>();
        zoomTarget = camera.fieldOfView;
    }

    // Update
    void Update()
    {

        zoomTarget -= Input.GetAxis("Mouse ScrollWheel") * multiplier;
        zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
        camera.fieldOfView = Mathf.SmoothDamp(camera.fieldOfView, zoomTarget, ref velocity, smoothTime);

    }
}
