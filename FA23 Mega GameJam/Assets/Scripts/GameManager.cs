using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameManager
{
    public static GameObject s_failScreen;
    public static GameObject s_winScreen;
    public static bool s_fail = false;  

    public static void EndLevel()
    {
        if (s_fail)
        {
            FailLevel();
        }
        else
        {
            WinLevel();
        }
    }

    private static void FailLevel()
    {
        SceneManage.Reload();
    }

    private static void WinLevel()
    {
        SceneManage.LoadNextScene();
    }
}
