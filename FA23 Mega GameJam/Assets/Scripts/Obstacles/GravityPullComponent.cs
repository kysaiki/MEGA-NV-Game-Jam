using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPullComponent : MonoBehaviour
{
    [SerializeField] private float Gravity = -10.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(CalculateGravitationForce(other.gameObject));
        }
    }

    Vector3 CalculateGravitationForce(GameObject player)
    {
        GameObject gravityPlanet = this.transform.parent.gameObject;
        float distance = Vector3.Distance(player.transform.position, gravityPlanet.transform.position);
        float gravityStrength = Gravity / (distance * distance);
        Vector3 playerToPlanetVector = gravityPlanet.transform.position - player.transform.position;
        return playerToPlanetVector * gravityStrength;
    }
}
