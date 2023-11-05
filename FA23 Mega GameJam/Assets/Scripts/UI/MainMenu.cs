using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        SceneManage.GetCurrScene();
    }
    public void StartGame()
    {
        SceneManage.LoadNextScene();
    }

    public void QuitGame()
    {
        SceneManage.Quit();
    }

    public void PlayCreditScreen()
    {
        SceneManage.LoadCredits();
    }
}
