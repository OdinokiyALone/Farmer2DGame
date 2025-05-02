
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class GameEntryPoint
{
    private static GameEntryPoint _instance;
    private Corutines _corutines;
    private UIManager _UIRoot;
    private Managers managers;
    //private MainManager mainManager;

    public string MainMenu;


    [RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void OnPlay()
    {
        if (true)
        {
            _instance = new GameEntryPoint();
            _instance._corutines.StartCoroutine(_instance.RunGame());
        }

    }

    private GameEntryPoint()
    {
        _corutines = new GameObject("[SYSTEM]").AddComponent<Corutines>();
    }
    private IEnumerator RunGame()
    {
        MainManager.Manager = new MainManager(_corutines.gameObject);
        #if UNITY_EDITOR
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0))
        {
            _corutines.StopCoroutine(RunGame());
            yield return null;
        }
        #endif

        yield return null;
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
