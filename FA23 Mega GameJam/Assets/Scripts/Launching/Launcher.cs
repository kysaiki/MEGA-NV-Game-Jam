using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1000.0f;
    [SerializeField] private Camera launcherCam;

    [SerializeField] private GameCameraControl LaunchCamera; 
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private Transform trajectoryStart;
    [SerializeField] private float trajectoryLength = 50f;
    [SerializeField] private LaunchTimerComponent LaunchTimer;

    [SerializeField] private RocketBehavior Rocket;
    private bool launched = false;

    private bool lockTrajectory = false;

    void Update()
    {
         // if we are not in launching perspective, do nothing
        if (!launcherCam.isActiveAndEnabled)
        {
            launched = false; // but reset this
            return;
        }
        
        if (!launched)
        {
            // Launch rocketship
            if (Input.GetKeyDown(KeyCode.Space)) {
                lockTrajectory = true;
                LaunchTimer.ActivateTimer();
            }
        }

        if (!lockTrajectory)
        {
            FollowMouse();
            DrawTrajectory();
        }

    }

    private void FollowMouse()
    {
        
        Plane groundPlane = new Plane(Vector3.up, 0f);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0f;
        Vector3 mousePos = mouseRay.origin;

        if (groundPlane.Raycast(mouseRay, out hitDist))
        {
            mousePos = mouseRay.GetPoint(hitDist);
            Vector3 toMouse = mousePos - transform.position;
            toMouse.y = 0f;
            if (toMouse.sqrMagnitude > float.Epsilon)
            {
                toMouse.Normalize();
                Quaternion goal = Quaternion.AngleAxis(Mathf.Atan2(toMouse.x, toMouse.z) * Mathf.Rad2Deg, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, goal, rotationSpeed * Time.deltaTime);
            }
        }
        
        // Debug.DrawLine(transform.position, mousePos, Color.green, 5f);
    }

    private void DrawTrajectory()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, trajectoryStart.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, trajectoryLength, targetLayers))
        {
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else 
        {
            lineRenderer.SetPosition(1, trajectoryStart.position + transform.forward * trajectoryLength);
        }
    }

    public void Launch()
    {
        Debug.Log("Launch!");
        launched = true;
        // disable line
        lineRenderer.positionCount = 0;
        // switch cameras?
        // launch rocket
        // RocketBehavior rocket = GetComponent<RocketBehavior>();
        AudioManager.instance.PlaySFX(AudioManager.SoundEffect.Launch);
        transform.DetachChildren();
        LaunchCamera.ActivateLaunchCamera();
        Rocket.Launch();
    }
}