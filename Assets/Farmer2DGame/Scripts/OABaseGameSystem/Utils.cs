using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Utils
{
    public static Vector2 ColliderToCameraSize(Vector2 size)
    {
        return Vector2.zero;
    }

    
}
public class FPSDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        float fps = 1.0f / deltaTime;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(Screen.width - 110, Screen.height - 30, 100, 25); 
        style.alignment = TextAnchor.LowerRight;
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        GUI.Label(rect, $"FPS: {fps:0.}", style);
    }
}

public class Corutines : MonoBehaviour
{
    private void Awake()
    {
        Object.DontDestroyOnLoad(this.gameObject);
    }
}

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _progressBar;
    private GameObject _loadingsScreen;
    private void Awake()
    {
        _loadingsScreen = Resources.Load<GameObject>("LoadingScreen");
        //_progressBar = _loadingsScreen.transform.GetComponentInChildren();
    }

    void Start()
    {
        Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted);

    }
    private void OnDestroy()
    {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagersStarted); ;
    }

    private void OnManagersProgress(int numReady, int numModules)
    {
        float progress = (float)numReady / numModules;
        _progressBar.value = progress;
    }

    private void OnManagersStarted()
    {
        Managers.Mission.GotoNext(ConstCollection.MAIN_MENU);
    }
}

#if UNITY_EDITOR
public static class ToggleMenuExample
{
    /// <summary>
    /// if true starts the project in normal mode
    /// </summary>
    public static bool IsPlaytest; 

    [MenuItem("Tools/Toggle Playtest")]
    private static void TogglePlaytest()
    {
        IsPlaytest = !IsPlaytest;
    }

    [MenuItem("Tools/Toggle Playtest", true)]
    private static bool ToggleStateValidate()
    {
        Menu.SetChecked("Tools/Toggle Playtest", IsPlaytest);
        return true;
    }
}
#endif
