using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject radarPulsePrefab;
    public LayerMask radarDetectLayer;

    [SerializeField] private float pulseMaxSize = 5.0f;
    [SerializeField] private float pulseSpeed = 5.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // Trigger pulse on a key press (adjust as needed)
        {
            StartCoroutine(ActivateRadarPulse());
        }
    }

    IEnumerator ActivateRadarPulse()
    {
        GameObject pulse = Instantiate(radarPulsePrefab, transform.position, Quaternion.identity);
        float pulseSize = 0.1f; // Initial size of the pulse

        while (pulseSize < pulseMaxSize) // Adjust the threshold for pulse size as needed
        {
            pulseSize += Time.deltaTime * pulseSpeed;
            pulse.transform.localScale = Vector3.one * pulseSize;
            yield return null;
        }

        Destroy(pulse); // Remove the pulse when it reaches the desired size
        DetectObjects();
    }

    void DetectObjects()
    {
        Vector3 radarCenter = transform.position;

        // Perform a sphere cast to detect objects within the radar pulse range
        Collider[] detectedObjects = Physics.OverlapSphere(radarCenter, 1f, radarDetectLayer);

        foreach (Collider obj in detectedObjects)
        {
            // Calculate the position of the detected object on the radar
            Debug.Log(obj.gameObject);
            Debug.Log(obj.transform.position);
            Vector3 radarPosition = transform.InverseTransformPoint(obj.transform.position);

            // Display the object on the HUD (e.g., create a marker)
            DisplayObjectOnHUD(radarPosition, obj.gameObject);
        }
    }

    void DisplayObjectOnHUD(Vector3 position, GameObject detectedObject)
    {
        Debug.Log("C");
        Debug.Log(position);
        // Create a UI element on the HUD to represent the detected object
        // You can instantiate icons or markers and position them using 'position'
    }
}
