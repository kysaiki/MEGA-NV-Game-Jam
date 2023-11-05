using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1000.0f;
    [SerializeField] private Camera launcherCam;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float trajectoryLength = 5f;

    void Update()
    {
        // if we are not in launching perspective, do nothing
        if (!launcherCam.isActiveAndEnabled)
            return;
        FollowMouse();
        DrawTrajectory();

        if (Input.GetKeyDown(KeyCode.Space))
            Launch();
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

    private void DrawTrajectory()
    {
        // lineRenderer.
    }

    public void Launch()
    {
        Debug.Log("Launch!");
    }
}