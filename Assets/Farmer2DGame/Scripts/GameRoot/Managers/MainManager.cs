using UnityEngine;

public class MainManager
{
    public static string SceneToLoad = null;

    public static MainManager Manager;
    private  Corutines corutines;
    public  GameObject SystemObject;

    public UIManager UIManager;
    public LevelManager LevelManager;
    public SaveManager SaveManager;
    public InputManager InputManager;

    public MainManager(GameObject SystemObject)
    {
        MainManager.Manager = this;
        MainManager.Manager.SystemObject = SystemObject;
        corutines = this.SystemObject.GetComponent<Corutines>();
        UIManager = new();
        LevelManager = new();
        SaveManager = new();
        InputManager = new();
    }

  
    public void GamePause()
    {

    }

    public void GameResume()
    {

    }
}