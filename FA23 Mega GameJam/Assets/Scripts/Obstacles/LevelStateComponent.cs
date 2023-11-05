using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateComponent : MonoBehaviour
{
    [SerializeField] private Transform LevelMinTransform;
    [SerializeField] private Transform LevelMaxTransform;
    [SerializeField] private GameObject Rocket;
    [SerializeField] private float LevelTime;
    [SerializeField] private int currLevel;
    [SerializeField] private GameCameraControl CameraControl;

    public GameObject FuelSlider;
    public GameObject failScreen;
    public GameObject winScreen;

    public int RequiredCollectibles = 0;
    bool RocketHitObstacle = false;
    float Timer = 0.0f;
    bool completed = false;

    private void Start()
    {
        RequiredCollectibles = GameManager.GetObjective(currLevel);
        AudioManager.instance.CrossfadeToTrack(AudioManager.MusicState.Aiming);
        FuelSlider.gameObject.SetActive(true);

    }

    void Update()
    {   
        if (RocketIsOutOfBounds() || LevelTimeExceeded())
        {
            failScreen.SetActive(true);
            FuelSlider.gameObject.SetActive(false);
            AudioManager.instance.PlaySFX(AudioManager.SoundEffect.Lose);
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

    bool AllCollectiblesCollected()
    {
        return RequiredCollectibles <= 0;
    }

    public void FinishLevel()
    {
        FuelSlider.gameObject.SetActive(false);
        if (!RocketIsOutOfBounds() && !LevelTimeExceeded() && AllCollectiblesCollected())
        {
            winScreen.SetActive(true);
            AudioManager.instance.PlaySFX(AudioManager.SoundEffect.Win);
            CameraControl.ActivateFinishCamera();
            Destroy(Rocket);
            completed = true;
            
        }
    }

    public void IncrementCollectible()
    {
        RequiredCollectibles++;
    }

    public void DecrementCollectible()
    {
        RequiredCollectibles--;
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.tag == "Obstacle") RocketHitObstacle = true;
    // }

    public void NextLevel()
    {
        GameManager.WinLevel();
    }

    public void FailLevel()
    {
        GameManager.FailLevel();
    }

    public void MainMenu()
    {
        SceneManage.LoadMainMenu();
    }
}
