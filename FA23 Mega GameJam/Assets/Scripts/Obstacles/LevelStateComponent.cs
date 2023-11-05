using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateComponent : MonoBehaviour
{
    [SerializeField] private Transform LevelMinTransform;
    [SerializeField] private Transform LevelMaxTransform;
    [SerializeField] private GameObject Rocket;
    [SerializeField] private float LevelTime;

    bool RocketHitObstacle = false;
    float Timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // if ()
        // {
        //     Timer += Time.deltaTime;
        // }
        
        if (RocketIsOutOfBounds() || RocketHitObstacle || LevelTimeExceeded())
        {
            Debug.Log("FAILED");
        }
    }

    bool LevelTimeExceeded()
    {
        return Timer > LevelTime;
    }

    bool RocketIsOutOfBounds()
    {
        bool MinXCheck = (Rocket.transform.position.x < LevelMinTransform.position.x);
        bool MaxXCheck = (Rocket.transform.position.x > LevelMaxTransform.position.x);
        bool MinZCheck = (Rocket.transform.position.z < LevelMinTransform.position.z);
        bool MaxZCheck = (Rocket.transform.position.z > LevelMaxTransform.position.z);
        return (MinXCheck || MaxXCheck || MinZCheck || MaxZCheck);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") RocketHitObstacle = true;
    }
}
