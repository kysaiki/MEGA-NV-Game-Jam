using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject failScreen;
    public GameObject winScreen;
    public int currLvl;
    private int objective;
    void Start()
    {
        GameManager.GetObjective(currLvl);
        objective = GameManager.s_currGoal;
    }

    void Update()
    {
        
    }

    public void NextLevel()
    {
        GameManager.WinLevel();
    }

    public void FailLevel()
    {
        GameManager.FailLevel();
    }
}
