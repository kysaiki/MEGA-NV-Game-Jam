using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitComponent : MonoBehaviour
{
    public Transform targetObject;
    public float orbitSpeed = 10.0f;
    public float orbitRadius = 5.0f;
    private Vector3 orbitAxis = Vector3.up;

    void Update()
    {
        // Perform the orbit around the target object
        Orbit();
    }

    void Orbit()
    {
        transform.RotateAround(targetObject.position, orbitAxis, orbitSpeed * Time.deltaTime);
        // You can also update the position using the orbit radius if needed:
        // transform.position = targetObject.position + (transform.position - targetObject.position).normalized * orbitRadius;
    }
}
