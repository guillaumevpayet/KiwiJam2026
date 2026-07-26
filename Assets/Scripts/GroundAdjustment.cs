using Unity.Cinemachine;
using UnityEngine;

public class GroundAdjustment : MonoBehaviour
{
    public CinemachineCamera camera;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 0.5)
        {
            camera.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value = -2;
        }
        else
        {
            camera.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value = -4.42f;
        }
    }
}
