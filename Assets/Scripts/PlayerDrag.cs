using System;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distance;

    private void Start()
    {
        transform.position = playerTransform.position + Vector3.down * distance;
    }

    private void Update()
    {
        var heightDifference = playerTransform.position.y - transform.position.y;

        if (heightDifference > distance)
        {
            transform.position = playerTransform.position + Vector3.down * distance;
        }
        else
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        }
    }
}
