using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameManager
{
    public static GameObject s_failScreen;
    public static GameObject s_winScreen;
    public static bool s_fail = false; 
    public static int s_currLevel = 0;
    public static int s_currGoal = -1;

/*    public static void EndLevel()
    {
        if (s_fail)
        {
            FailLevel();
        }
        else
        {
            WinLevel();
        }
    }*/

    public static void FailLevel()
    {
        AudioManager.instance.StopEngineSound();
        SceneManage.Reload();
    }

    public static void WinLevel()
    {
        AudioManager.instance.StopEngineSound();
        SceneManage.LoadNextScene();
    }

    public static int GetObjective(int lvl)
    {
        switch (lvl)
        {
            case 1:
                s_currGoal = 0;
                break;
            case 2:
                s_currGoal = 1;
                break;
            case 3:
                s_currGoal = 3;
                break;
            case 4:
                s_currGoal = 0;
                break;
            case 5:
                s_currGoal = 1;
                break;
            case 6:
                s_currGoal = 3;
                break;
            case 7:
                s_currGoal = 0;
                break;
            case 8:
                s_currGoal = 0;
                break;
            default:
                break;
        }

        return s_currGoal;
    }
}
