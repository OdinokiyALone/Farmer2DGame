using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    public Scene activeScene { get; private set; }

    
    public void LoadScene(Scene scene)
    {
        
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(ConstCollection.BOOT, LoadSceneMode.Single);
        SceneManager.LoadScene(name, LoadSceneMode.Single);

    }

    public void UnloadScene(Scene scene)
    {

    }



}