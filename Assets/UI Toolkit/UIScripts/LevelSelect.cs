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
    private Button level3Button;
    private Button level4Button;
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

        level3Button = root.Q<Button>("Level3");
        level3Button.RegisterCallback<ClickEvent>(OnLevel3Click);

        level4Button = root.Q<Button>("Level4");
        level4Button.RegisterCallback<ClickEvent>(OnLevel4Click);

        returnButton = root.Q<Button>("Return");
        returnButton.RegisterCallback<ClickEvent>(OnReturnClick);
    }

    void OnDisable()
    {
        level1Button.UnregisterCallback<ClickEvent>(OnLevel1Click);
        level2Button.UnregisterCallback<ClickEvent>(OnLevel2Click);
        level3Button.UnregisterCallback<ClickEvent>(OnLevel3Click);
        level4Button.UnregisterCallback<ClickEvent>(OnLevel4Click);
        returnButton.UnregisterCallback<ClickEvent>(OnReturnClick);
    }

    private void OnLevel1Click(ClickEvent evt)
    {
        CurrentLevel.Level = "Level4";
        SceneManager.LoadScene("Level4");
    }

    private void OnLevel2Click(ClickEvent evt)
    {
        CurrentLevel.Level = "Level2";
        SceneManager.LoadScene("Level2");
    }
    private void OnLevel3Click(ClickEvent evt)
    {
        CurrentLevel.Level = "game";
        SceneManager.LoadScene("game");
    }

    private void OnLevel4Click(ClickEvent evt)
    {
        CurrentLevel.Level = "level3";
        SceneManager.LoadScene("Level3");
    }

    private void OnReturnClick(ClickEvent evt)
    {
        menu.rootVisualElement.visible = true;
        root.visible = false;
    }
}
