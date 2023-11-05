using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlanetComponent : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45.0f;
    [SerializeField] private CameraMovement LaunchCamera; 
    [SerializeField] private LaunchTimerComponent LaunchTimer;

    public RocketBehavior Rocket;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Q)) transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKeyDown(KeyCode.Space)) LaunchTimer.ActivateTimer();
    }

    public void Launch()
    {
        transform.DetachChildren();
        LaunchCamera.ActivateLaunchCamera();
        Rocket.Launch();
    }
}
