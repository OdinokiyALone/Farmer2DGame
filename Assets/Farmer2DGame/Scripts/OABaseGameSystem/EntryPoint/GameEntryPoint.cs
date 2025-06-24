using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace OABaseGameSystem
{


    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Corutines _corutines;
        private UIManager _UIRoot;
        private MainManager managers;
        //private MainManager mainManager;

        public string MainMenu;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
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
}

