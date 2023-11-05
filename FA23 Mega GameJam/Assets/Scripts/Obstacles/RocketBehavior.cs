using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 0.01f;
    public float m_Speed = 10.0f;
    bool launched = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (launched)
        {
            float rotationSpeed = 90.0f;
            m_Rigidbody.AddForce(transform.up * m_Thrust);
        }
    }

    public void Launch()
    {
        launched = true;
        for (int i = 0; i < 30; i++)
        {
            m_Rigidbody.AddForce(transform.up * m_Thrust, ForceMode.Impulse);
        }
    }
}
