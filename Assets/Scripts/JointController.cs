using UnityEngine;

public class JointController : MonoBehaviour
{
    [SerializeField] private float elasticity = 1f;

    private SpringJoint _springJoint;

    public void Initialize(Rigidbody connectedBody, int index)
    {
        _springJoint.connectedBody = connectedBody;
        _springJoint.maxDistance = Vector3.Distance(transform.position, connectedBody.transform.position);
    }
    
    private void Awake()
    {
        _springJoint = GetComponent<SpringJoint>();
    }

    private void FixedUpdate()
    {
        _springJoint.maxDistance = Mathf.Pow(0.9f, elasticity) * _springJoint.maxDistance;
    }
}
