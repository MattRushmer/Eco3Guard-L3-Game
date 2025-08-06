using UnityEngine;

public class PollutionExpansion : MonoBehaviour
{
    public float expansionRate = 0.01f;
    public Vector3 maxScale = new Vector3(5.3f, 5.3f, 5.3f);
    public float delay = 10f;
    public Material pollutionMaterial;
    
    private float timer = 0f;
    private Vector3 initialScale;

    public Transform earth;
    public Transform pollution; 

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;
        pollution.rotation = earth.rotation; // Linking the Earth and Pollution rotation for visual syncing purposes.

        if (timer >= delay)
        {
            if (transform.localScale.x < maxScale.x ||
                transform.localScale.y < maxScale.y ||
                transform.localScale.z < maxScale.z)
            {
                Vector3 newScale = transform.localScale + Vector3.one * expansionRate * Time.deltaTime;
                transform.localScale = Vector3.Min(newScale, maxScale);
            }
        }
    }
}