using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolComponent : MonoBehaviour
{
    [SerializeField] private PatrolPoint[] waypoints;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        // Initialize the first waypoint as the target
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].transform.position;
            currentWaypointIndex = 0;
        }
    }

    private void Update()
    {
        if (waypoints.Length == 0)
            return;

        // Move the UFO towards the current waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, waypoints[currentWaypointIndex].speed * Time.deltaTime);

        // Check if the UFO has reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

[System.Serializable]
public struct PatrolPoint
{
    public Transform transform;
    public float speed;
}