using UnityEngine;

public class HingeJointSegment : MonoBehaviour
{
    [SerializeField] private Renderer _cylinder;
    [SerializeField] private Renderer _ball;
    
    public void Initialize(Material material)
    {
        _cylinder.material = material;
        _ball.material = material;
    }
}
