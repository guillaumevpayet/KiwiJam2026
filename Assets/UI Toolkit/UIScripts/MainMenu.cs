using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public UIDocument UIDoc;
    public UIDocument select;
    private VisualElement root;
    private Button levelButton;
    private Button quitButton;

    private void OnEnable()
    {
        root = UIDoc.rootVisualElement;

        levelButton = root.Q<Button>("LevelSelect");
        levelButton.RegisterCallback<ClickEvent>(OnLevelSelectClick);

        quitButton = root.Q<Button>("Quit");
        quitButton.RegisterCallback<ClickEvent>(OnQuitClick);
    }
    void Start()
    {
        levelButton = root.Q<Button>("LevelSelect");
        levelButton.RegisterCallback<ClickEvent>(OnLevelSelectClick);

        quitButton = root.Q<Button>("Quit");
        quitButton.RegisterCallback<ClickEvent>(OnQuitClick);

        select.rootVisualElement.visible = false;
    }

    void OnDisable()
    {
        levelButton.UnregisterCallback<ClickEvent>(OnLevelSelectClick);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitClick);
    }

    private void OnLevelSelectClick(ClickEvent evt)
    {
        select.rootVisualElement.visible = true;
        root.visible = false;
    }

    private void OnQuitClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
