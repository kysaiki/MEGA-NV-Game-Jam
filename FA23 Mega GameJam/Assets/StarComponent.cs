using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarComponent : MonoBehaviour
{
    [SerializeField] private LevelStateComponent LevelState;

    // Start is called before the first frame update
    void Start()
    {
        LevelState.IncrementCollectible();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collected");
            LevelState.DecrementCollectible();
            Destroy(this.gameObject);
        }
    }
}
