using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaunchTimerComponent : MonoBehaviour
{
    [SerializeField] private float CountdownTime;
    [SerializeField] private TMP_Text CountdownText;
    [SerializeField] private StartPlanetComponent LaunchPlanet;

    bool activateTimer = false;
    float timer = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if (activateTimer)
        {
            timer -= Time.deltaTime;
            UpdateText("T - " + timer.ToString());
            if (timer <= 0.0f)
            {
                UpdateText("LAUNCH");
                LaunchPlanet.Launch();
                Destroy(gameObject);
            }
        }
    }

    void UpdateText(string text)
    {
        CountdownText.text = text;
    }

    public void ActivateTimer()
    {
        activateTimer = true;
        timer = CountdownTime;
    }
}
