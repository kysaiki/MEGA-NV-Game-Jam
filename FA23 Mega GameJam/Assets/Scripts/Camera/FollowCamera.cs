using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform m_target = null;
    public Transform m_rig = null;

    public float m_distance = 10f;
    public float m_rotationSpeed = 10f;

    Vector3 m_cameraPosition;
    Vector3 m_smoothPosition;

    float m_smoothTime = 0.125f;
    float m_angle;

    private int m_camType = 0;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            m_camType += 1;
            if(m_camType >= 3)
            {
                m_camType = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        m_cameraPosition = m_target.position - (m_target.forward * m_distance) + m_target.up * m_distance * 0.25f;
        m_smoothPosition = Vector3.Lerp(transform.position, m_cameraPosition, m_smoothTime);
        transform.position = m_smoothPosition;

        m_angle = Mathf.Abs(Quaternion.Angle(transform.rotation, m_target.rotation));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, m_target.rotation, (m_rotationSpeed + m_angle) * Time.deltaTime);
        /*
        if(m_camType == 0)
        {
            RigCam();
        }
        else if(m_camType == 1)
        {
            TopCam();
        }
        else if (m_camType == 3)
        {
            BackCam();
        }
        else
        {
            RigCam();
        }
        */
    }

    void RigCam()
    {
        transform.position = m_rig.position;
        transform.rotation = m_rig.rotation;
    }

    void TopCam()
    {
        transform.position = m_target.position + m_target.up * m_distance + -1 * m_target.forward * (m_distance / 3.0f);
        transform.rotation = m_target.rotation * Quaternion.Euler(90f, 0, 0);

    }

    void BackCam()
    {
    }
}
