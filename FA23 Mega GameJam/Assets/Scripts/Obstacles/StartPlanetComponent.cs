using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlanetComponent : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45.0f;
    [SerializeField] private GameCameraControl LaunchCamera; 
    [SerializeField] private LaunchTimerComponent LaunchTimer;

    public RocketBehavior Rocket;

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        if (Input.GetKeyDown(KeyCode.Space)) LaunchTimer.ActivateTimer();
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
                if (toMouse.sqrMagnitude > float.Epsilon)
                {
                    Quaternion goal = Quaternion.AngleAxis(Mathf.Atan2(toMouse.x, toMouse.z) * Mathf.Rad2Deg, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, goal, rotationSpeed * Time.deltaTime);
                }
            }
        }
        
        Debug.DrawLine(transform.position, mousePos, Color.green, 5f);
    }

    public void Launch()
    {
        transform.DetachChildren();
        LaunchCamera.ActivateLaunchCamera();
        Rocket.Launch();
    }
}
