using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHud : MonoBehaviour
{
    public UIDocument UIDoc;
    public int hpDrainInterval;
    public GameObject player;
    public int victoryHeight;
    public Texture alive;
    public Texture dead;


    private List<Image> hpPips = new List<Image>();
    private int hpLeft = 5;
    private VisualElement root;
    private ProgressBar levelProgress;
    private Slider levelProgress2;


    private void OnEnable()
    {
        root = UIDoc.rootVisualElement;
    }
    private void Start()
    {
        hpPips.Add(root.Q<Image>("Pip1"));
        hpPips.Add(root.Q<Image>("Pip2"));
        hpPips.Add(root.Q<Image>("Pip3"));
        hpPips.Add(root.Q<Image>("Pip4"));
        hpPips.Add(root.Q<Image>("Pip5"));
        InvokeRepeating(nameof(HealthDrain),hpDrainInterval,hpDrainInterval);

        levelProgress = root.Q<ProgressBar>("LevelProgress");
        levelProgress.highValue = victoryHeight;
        levelProgress2 = root.Q<Slider>("LevelProgress2");
        levelProgress2.highValue = victoryHeight;
    }

    private void Update()
    {
        float height = player.transform.position.y;
        if (victoryHeight < height)
        {
            SceneManager.LoadScene("Victory");
        }
        levelProgress.value = player.transform.position.y;
        levelProgress2.value = player.transform.position.y;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Defeat");
        }
    }

    private void HealthDrain()
    {
        HealthChanged(false);
    }


    void HealthChanged(bool direction)
    {
        if (direction)
        {
            if (hpLeft < 5)
            {
                hpPips[hpLeft].image = alive;
                hpLeft += 1;
            }
        }
        else
        {
            if (hpLeft <= 1)
            {
                SceneManager.LoadScene("Defeat");
            }
            else
            {
                hpLeft -= 1;
                hpPips[hpLeft].image = dead;
            }
        }
    }


}
