using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class UIManager
{
    private Dictionary<string, UIObject> activeUi;
    private UIRoot uIRoot;

    private PanelSettings PanelSettings;

    public UIManager()
    {
        PanelSettings = Resources.Load<PanelSettings>("Panel Settings");
        activeUi = new Dictionary<string, UIObject>();
        CreateUIRoot();
    }

    private void CreateUIRoot()
    {
        GameObject uiroot = GameObject.Instantiate(new GameObject("UIRoot"), MainManager.Manager.SystemObject.transform, false);
        uIRoot = uiroot.AddComponent<UIRoot>().GetComponent<UIRoot>();
        Debug.Log("Create UIRoot");
        uIRoot.name = "[UIROOT]";

        //GameObject.DontDestroyOnLoad(uIRoot.gameObject);
    }

    public void ShowUI(string UIname)
    {
        if (!activeUi.TryGetValue(UIname, out UIObject uI) || uI == null)
        {
            LoadUIObject(UIname);
            uI = activeUi[UIname];
        }

        if (uI.pauseOnActive && MainManager.Manager != null)
        {
            MainManager.Manager.GamePause();
        }

        uI.Enable();
    }

    public void HideUI(string UIname)
    {
        if (activeUi.TryGetValue(UIname, out UIObject ui))
        {
            if (ui.pauseOnActive && MainManager.Manager != null)
            {
                MainManager.Manager.GameResume();
            }
            ui.Disable();
        }
    }

    public void LoadUIObject(string name)
    {
        // Пытаемся найти класс UIObject
        Type type = Type.GetType("UI" + name);
        if (type == null)
        {
            Debug.LogError($"Тип {name} не найден! Убедись, что имя класса правильно и он в глобальном пространстве имён.");
            return;
        }

        // Создаём объект
        GameObject go = new GameObject(name);
        go.transform.SetParent(uIRoot.transform);

        // Добавляем UIDocument
        var uiDocument = go.AddComponent<UIDocument>();

        // Загружаем VisualTreeAsset
        VisualTreeAsset vta = Resources.Load<VisualTreeAsset>(name);
        if (vta == null)
        {
            Debug.LogError($"VisualTreeAsset с именем {name} не найден в папке Resources!");
            return;
        }

        uiDocument.visualTreeAsset = vta;
        uiDocument.panelSettings = PanelSettings;

        // Добавляем UIObject (конкретный тип, например UIMainMenu)
        var uIObject = go.AddComponent(type) as UIObject;
        if (uIObject == null)
        {
            Debug.LogError($"Компонент {type.Name} не является наследником UIObject!");
            return;
        }

        uIObject.root = uiDocument.rootVisualElement;
        uIObject.ParentUIManager = this;
        uIObject.gameObject.SetActive(false);

        // Добавляем в активный список
        activeUi[name] = uIObject;
    }
   
    
    
    /*
        private void LoadUIObject(string name)
        {
            GameObject go = GameObject.Instantiate(new GameObject(name), uIRoot.gameObject.transform);
            Type type = Type.GetType("UI" + name);


            go.AddComponent<UIDocument>();
            go.AddComponent(type);


            UIObject uIObject = go.GetComponent(type) as UIObject;

            uIObject.gameObject.SetActive(false);
            uIObject.gameObject.name = name;
            uIObject.GetComponent<UIDocument>().visualTreeAsset = Resources.Load<VisualTreeAsset>(name);
            uIObject.GetComponent<UIDocument>().panelSettings = PanelSettings;
            uIObject.root = uIObject.GetComponent<UIDocument>().rootVisualElement;
            uIObject.ParentUIManager = this;


            activeUi[name] = uIObject;
            ShowUI(name);
        }
    */




    public void UnloadUIObject(UIObject uI)
    {
        if (activeUi.ContainsKey(uI.name))
        {
            activeUi.Remove(uI.name);
        }
    }
}

public class UIObject : MonoBehaviour
{
    public bool pauseOnActive;
    public UIManager ParentUIManager { get; set; }
    public VisualTreeAsset VisualTree;
    public VisualElement root;

    private void OnDestroy()
    {
        ParentUIManager?.UnloadUIObject(this);
    }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}

