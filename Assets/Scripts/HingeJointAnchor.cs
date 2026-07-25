using UnityEngine;

public class HingeJointAnchor : MonoBehaviour
{
    private Transform _ball;

    public void Initialize(Transform ball)
    {
        _ball = ball;
    }

    private void Update()
    {
        transform.position = _ball.position;
    }
}
