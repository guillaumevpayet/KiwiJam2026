using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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
        
        // Loading the video
        var videoPath = System.IO.Path.Combine(
            Application.streamingAssetsPath, 
            "defeat_animation.webm"
        );
        
        var videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }

    private void OnDisable()
    {
        retryButton.UnregisterCallback<ClickEvent>(OnRetryGameClick);
        menuButton.UnregisterCallback<ClickEvent>(OnReturnToMenuClick);
    }

    private void OnRetryGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene(CurrentLevel.Level);
    }

    private void OnReturnToMenuClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Menu");
    }


}
