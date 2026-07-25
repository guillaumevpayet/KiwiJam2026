using UnityEngine;

public class HingeJointController : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private Material[] materials;

    private HingeJoint _hingeJoint;
    
    private Rigidbody _connectedBody;
    private GameObject[] _segments;

    private float _length;
    private int _segmentCount;
    private int _index;

    public void Initialize(Rigidbody connectedBody, int index)
    {
        _connectedBody = connectedBody;
        _index = index;
        var connectionVector = transform.position - connectedBody.transform.position;
        _length = connectionVector.magnitude;
        var direction = connectionVector / _length;
        _segmentCount = Mathf.CeilToInt(_length / 0.5f);
        _segments = new GameObject[_segmentCount];

        for (var i = 0; i < _segmentCount; i++)
        {
            var segmentPosition = connectedBody.transform.position + 0.5f * i * direction;
            var segment = Instantiate(segmentPrefab, segmentPosition, Quaternion.FromToRotation(Vector3.up, direction));
            segment.transform.parent = transform;
            var segmentJoint = segment.GetComponent<HingeJoint>();
            segmentJoint.connectedBody = i == 0 ? connectedBody : _segments[i - 1].GetComponent<Rigidbody>();
            segment.GetComponentInChildren<MeshRenderer>().material = materials[index];
            _segments[i] = segment;
        }

        _hingeJoint.connectedBody = _segments[_segmentCount - 1].GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void FixedUpdate()
    {
        var connectionVector = transform.position - _connectedBody.transform.position;
        _length = connectionVector.magnitude;
        var segmentCount = Mathf.CeilToInt(_length / 0.5f);

        if (segmentCount == _segmentCount)
        {
            return;
        }

        foreach (var segment in _segments)
        {
            Destroy(segment);
        }
        
        _segments = new GameObject[_segmentCount];
        var direction = connectionVector / _length;

        for (var i = 0; i < _segmentCount; i++)
        {
            var segmentPosition = _connectedBody.transform.position + 0.5f * i * direction;
            var segment = Instantiate(segmentPrefab, segmentPosition, Quaternion.FromToRotation(Vector3.up, direction));
            segment.transform.parent = transform;
            var segmentJoint = segment.GetComponent<HingeJoint>();
            segmentJoint.connectedBody = i == 0 ? _connectedBody : _segments[i - 1].GetComponent<Rigidbody>();
            segment.GetComponentInChildren<MeshRenderer>().material = materials[_index];
            _segments[i] = segment;
        }

        _segmentCount = segmentCount;
    }
}
