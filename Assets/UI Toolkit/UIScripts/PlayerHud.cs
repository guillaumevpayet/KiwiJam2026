using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHud : MonoBehaviour
{
    public UIDocument UIDoc;
    public int hpDrainInterval;
    public GameObject player;
    public int victoryHeight;

    private List<Image> hpPips = new List<Image>();
    private int hpLeft = 5;
    private VisualElement root;
    private ProgressBar levelProgress;


    private void OnEnable()
    {
        root = UIDoc.rootVisualElement;
    }
    private void Start()
    {
        hpPips.Add(root.Q("Pip1") as Image);
        hpPips.Add(root.Q<Image>("Pip2"));
        hpPips.Add(root.Q<Image>("Pip3"));
        hpPips.Add(root.Q<Image>("Pip4"));
        hpPips.Add(root.Q<Image>("Pip5"));
        InvokeRepeating(nameof(HealthDrain),hpDrainInterval,hpDrainInterval);

        levelProgress = root.Q<ProgressBar>("LevelProgress");
        levelProgress.highValue = victoryHeight;

    }

    private void Update()
    {
        float height = player.transform.position.y;
        if (victoryHeight < height)
        {
            SceneManager.LoadScene("Victory");
        }
        levelProgress.value = player.transform.position.y;
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
                hpPips[hpLeft].visible = true;
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
                hpPips[hpLeft].visible = false;
            }
        }
    }


}
