using UnityEngine;

public class PollutionExpansion : MonoBehaviour
{
    public float expansionRate = 1f;
    public Vector3 maxScale = new Vector3(47.8f, 47.8f, 47.8f);
    public float delay = 5f;
    public Material pollutionMaterial;
    
    private float timer = 0f;
    private Vector3 initialScale;
    
    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            if (transform.localScale.x < maxScale.x ||
                transform.localScale.y < maxScale.y ||
                transform.localScale.z < maxScale.z)
            {
                Vector3 newScale = transform.localScale + Vector3.one * expansionRate * Time.deltaTime;
                transform.localScale = Vector3.Min(newScale, maxScale);

                // Making the pollution more metallic, darker shade, as it expands
                float progress = (transform.localScale.x - initialScale.x) / (maxScale.x - initialScale.x);
                float metallicValue = Mathf.Lerp(0f, 0.3f, progress);
                pollutionMaterial.SetFloat("_Metallic", metallicValue);
            }
        }
    }
}