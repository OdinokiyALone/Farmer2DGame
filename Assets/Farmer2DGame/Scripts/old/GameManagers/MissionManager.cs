using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ConstCollection.ManagerStatus status { get; private set; }
  
    public string �urLevel { get; private set; }


    public void Startup()
    {
        Debug.Log("Mission manager starting...");
        status = ConstCollection.ManagerStatus.Started;
    }

    public void GotoNext(string nextScene )
    {
        �urLevel = nextScene;
        SceneManager.LoadScene(nextScene);
    }

    public void RestartCurrent()
    {
        SceneManager.LoadScene(�urLevel);
    }

    public void UpdateData(string curScene)
    {
        �urLevel = curScene;
    }


}
