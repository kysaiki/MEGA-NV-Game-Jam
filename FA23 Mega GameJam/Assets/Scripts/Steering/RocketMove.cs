using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    public float m_speed = 10.0f;
    public float m_rollSpeed = 100.0f;
    public float m_pitchSpeed = 100.0f;

    float m_roll;
    float m_pitch;
    float m_yaw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_roll = -1* Input.GetAxisRaw("Horizontal");
        m_pitch = -1 * Input.GetAxisRaw("Vertical");

        transform.Rotate(Vector3.forward * m_roll * m_rollSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.right * m_pitch * m_pitchSpeed * Time.deltaTime, Space.Self);

        if(Input.GetKey(KeyCode.Space))
        {
            // Note: Made to use Up vector due to Maya asset
            transform.Translate(Vector3.up * m_speed * Time.deltaTime);
        }
    }
}
