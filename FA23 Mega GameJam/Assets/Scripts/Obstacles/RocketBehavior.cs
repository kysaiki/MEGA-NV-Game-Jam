using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketBehavior : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 0.01f;
    public float m_Speed = 10.0f;
    bool launched = false;
    public GameObject failScreen;

    Rigidbody m_rb;

    public Slider FuelMeter;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (launched)
        {
            float rotationSpeed = 90.0f;
            
            if (Input.GetKey(KeyCode.Space) && FuelMeter.value > 0.0f && m_rb.velocity.magnitude < 100.0f)
            {
                FuelMeter.value -= 0.0025f;
                Debug.Log("BOOST");
                m_Rigidbody.AddForce(transform.up * m_Thrust * 100.0f * Time.deltaTime, ForceMode.Impulse);
            }
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

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Obstacle")
        {
            failScreen.SetActive(true);
            Destroy(this.gameObject);
            Debug.Log("COLLIDED");
        }
    }
}
