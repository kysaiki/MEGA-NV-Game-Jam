using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    public float m_torque = 10.0f;
    public float m_rollSpeed = 100.0f;
    public float m_pitchSpeed = 100.0f;

    float m_roll;
    float m_pitch;
    float m_yaw;

    GameObject m_modelObject;
    Rigidbody m_rb;
    Vector2 m_cursorPos;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_modelObject = GameObject.Find("Rocket");
    }

    // Update is called once per frame
    void Update()
    {
        m_cursorPos = CursorPosition();
        YawRoll();

    }

    private void FixedUpdate()
    {
        m_roll = -1 * Input.GetAxisRaw("Horizontal");
        m_pitch = m_cursorPos.y;
        m_yaw = m_cursorPos.x;
        float throttle = -1 * Input.GetAxisRaw("Vertical");

        m_rb.AddRelativeTorque(Vector3.back * m_torque * m_roll);
        m_rb.AddRelativeTorque(Vector3.right * m_torque * m_pitch);
        m_rb.AddRelativeTorque(Vector3.up * m_torque * m_yaw);

    }

    private Vector2 CursorPosition()
    {
        Vector2 cursorPosition = Input.mousePosition;
        cursorPosition.x -= Screen.width / 2;
        cursorPosition.y -= Screen.height / 2;

        //float cursorX = cursorPosition.x / (Screen.width / 2f);
        //float cursorY = cursorPosition.y / (Screen.height / 2f);

        return new Vector2(cursorPosition.x, cursorPosition.y);
    }

    private Vector3 CursorAngle()
    {
        Vector2 cursorAdjust = new Vector2(m_cursorPos.x, m_cursorPos.y * 0.25f);
        float angle = Vector2.SignedAngle(cursorAdjust, Vector2.down);
        if(cursorAdjust.x == 0)
        {
            return Vector3.zero;
        }

        return new Vector3(0, 0, angle / 4.0f);
    }

    private void YawRoll()
    {
        Vector3 targetRollValue = new Vector3(0, 180, Mathf.Clamp(-CursorAngle().z, -60, 60));

        m_modelObject.transform.localRotation = Quaternion.Euler(targetRollValue);
    }

}
