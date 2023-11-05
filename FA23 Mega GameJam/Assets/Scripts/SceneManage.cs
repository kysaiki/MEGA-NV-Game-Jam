using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManage
{
    public static int s_currSceneIdx;
    public static int s_lastSceneIdx;

    public static void Load(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void Reload()
    {
        SceneManager.LoadScene(s_currSceneIdx);
    }

    public static void LoadNextScene()
    {
        if(s_currSceneIdx < s_lastSceneIdx)
        {
            SceneManager.LoadScene(++s_currSceneIdx);
        }
    }

    public static void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
