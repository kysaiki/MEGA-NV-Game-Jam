using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    [SerializeField] private float creditPlayTime;

    private bool isPlaying;
    private float creditTimeCount = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            creditTimeCount += Time.deltaTime;
            if (creditTimeCount > creditPlayTime)
            {
                isPlaying = false;
                SceneManage.LoadMainMenu();
            }
        }
    }
}
