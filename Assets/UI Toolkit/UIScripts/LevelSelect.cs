using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{
    public UIDocument UIDoc;
    public UIDocument menu;
    private VisualElement root;
    private Button level1Button;
    private Button level2Button;
    private Button returnButton;

    private void OnEnable()
    {
        root = UIDoc.rootVisualElement;
    }
    void Start()
    {
        level1Button = root.Q<Button>("Level1");
        level1Button.RegisterCallback<ClickEvent>(OnLevel1Click);

        level2Button = root.Q<Button>("Level2");
        level2Button.RegisterCallback<ClickEvent>(OnLevel2Click);

        returnButton = root.Q<Button>("Return");
        returnButton.RegisterCallback<ClickEvent>(OnReturnClick);
    }

    void OnDisable()
    {
        level1Button.UnregisterCallback<ClickEvent>(OnLevel1Click);
        level2Button.UnregisterCallback<ClickEvent>(OnLevel1Click);
        returnButton.UnregisterCallback<ClickEvent>(OnReturnClick);
    }

    private void OnLevel1Click(ClickEvent evt)
    {
        CurrentLevel.Level = "Game";
        SceneManager.LoadScene("Game");
    }

    private void OnLevel2Click(ClickEvent evt)
    {
        CurrentLevel.Level = "Level2";
        SceneManager.LoadScene("Level2");
    }

    private void OnReturnClick(ClickEvent evt)
    {
        menu.rootVisualElement.visible = true;
        root.visible = false;
    }
}
