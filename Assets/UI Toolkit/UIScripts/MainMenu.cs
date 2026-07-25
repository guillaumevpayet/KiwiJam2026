using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public UIDocument UIDoc;
    private VisualElement root;
    private Button levelButton;
    private Button quitButton;

    private void OnEnable()
    {
        root = UIDoc.rootVisualElement;
    }
    void Start()
    {
        levelButton = root.Q<Button>("Retry");
        levelButton.RegisterCallback<ClickEvent>(OnLevelSelectClick);

        quitButton = root.Q<Button>("Menu");
        quitButton.RegisterCallback<ClickEvent>(OnQuitClick);

    }

    void OnDisable()
    {
        levelButton.UnregisterCallback<ClickEvent>(OnLevelSelectClick);
        quitButton.UnregisterCallback<ClickEvent>(OnQuitClick);
    }

    private void OnLevelSelectClick(ClickEvent evt)
    {

    }

    private void OnQuitClick(ClickEvent evt)
    {

    }
}
