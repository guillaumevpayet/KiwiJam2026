using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    public UIDocument UIDoc;
    private VisualElement root;
    private Button retryButton;
    private Button menuButton;

    private void OnEnable()
    {
        root = UIDoc.rootVisualElement;
    }
    void Start()
    {
        retryButton = root.Q<Button>("Retry");
        retryButton.RegisterCallback<ClickEvent>(OnRetryGameClick);

        menuButton = root.Q<Button>("Menu");
        menuButton.RegisterCallback<ClickEvent>(OnReturnToMenuClick);
    }

    private void OnDisable()
    {
        retryButton.UnregisterCallback<ClickEvent>(OnRetryGameClick);
        menuButton.UnregisterCallback<ClickEvent>(OnReturnToMenuClick);
    }

    private void OnRetryGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Game");
    }

    private void OnReturnToMenuClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Main");
    }


}
