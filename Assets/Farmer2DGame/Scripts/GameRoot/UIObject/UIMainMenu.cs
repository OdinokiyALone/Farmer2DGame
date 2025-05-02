using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

 
public class UIMainMenu : UIObject
{
    private Button PlayButton;
    private Button LoadButton;
    private Button OptionsButton;
    private Button ExitButton;



    private void Awake()
    {
        root = this.gameObject.GetComponent<UIDocument>().rootVisualElement;
        PlayButton = root.Q<Button>("Play-Button");
        LoadButton = root.Q<Button>("Load-Button");
        OptionsButton = root.Q<Button>("Options-Button");
        ExitButton = root.Q<Button>("Exit-Button");
        //
        PlayButton.style.display = DisplayStyle.Flex;
        LoadButton.style.display = DisplayStyle.Flex;
        OptionsButton.style.display = DisplayStyle.Flex;
        ExitButton.style.display = DisplayStyle.Flex;
    }

    void Start()
    {
        Debug.Log($"PlayButton visible: {PlayButton.visible}, display: {PlayButton.style.display}");
    }

    private void OnEnable()
    {
        root = this.gameObject.GetComponent<UIDocument>().rootVisualElement;
        PlayButton = root.Q<Button>("Play-Button");
        LoadButton = root.Q<Button>("Load-Button");
        OptionsButton = root.Q<Button>("Options-Button");
        ExitButton = root.Q<Button>("Exit-Button");
        SubscribeButton();
    }

    private void OnDisable()
    {
        UnSubscribeButton();
    }

    void SubscribeButton()
    {
        Debug.Log("subscribe");
        PlayButton.clicked += OnPlayButton;
        LoadButton.clicked += OnLoadButton;
        OptionsButton.clicked += OnOptionsButton;
        ExitButton.clicked += OnExitButton;
    }

    void UnSubscribeButton()
    {
        Debug.Log("unsubscribe");
        PlayButton.clicked -= OnPlayButton;
        LoadButton.clicked -= OnLoadButton;
        OptionsButton.clicked -= OnOptionsButton;
        ExitButton.clicked -= OnExitButton;
    }

    void OnPlayButton()
    {
        Debug.Log("load scene");
        //MainManager.Manager.LevelManager.ChangeScene(ConstCollection.FOREST);
    }
    void OnOptionsButton() { Debug.Log("option press"); }
    void OnExitButton() { Debug.Log("exit press"); }
    void OnLoadButton() { Debug.Log("load press"); }


}
