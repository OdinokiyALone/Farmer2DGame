using OABaseGameSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIRoot : MonoBehaviour
{
    private UIDocument UIDocument;

    private UIManager parentManager;

    public void SetParent(UIManager uiManager)
    {
        parentManager = uiManager;
    }

    private void Awake()
    {
        //GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void SetUI(UIDocument UIDocument)
    {
        this.UIDocument = UIDocument;
    }
}