using UnityEngine;

public class HingeJointController : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private Material[] materials;
    [SerializeField] private float segmentSize = 0.15f;
    
    private HingeJointAnchor _anchor;
    private GameObject[] _segments;

    private float _length;
    private int _segmentCount;
    private int _index;

    public void Initialize(HingeJointAnchor anchor, int index)
    {
        _anchor = anchor;
        _index = index;
        var connectionVector = _anchor.transform.position - transform.position;
        _length = connectionVector.magnitude;
        _segmentCount = Mathf.CeilToInt(_length / (2f * segmentSize));
        var direction = connectionVector / _length;
        RecalculateSegments(direction);
    }

    private void FixedUpdate()
    {
        var connectionVector = transform.position - _anchor.transform.position;
        _length = connectionVector.magnitude;
        var segmentCount = Mathf.CeilToInt(_length / (2f * segmentSize));

        if (segmentCount == _segmentCount)
        {
            return;
        }

        foreach (var segment in _segments)
        {
            Destroy(segment);
        }

        _segmentCount = segmentCount;
        var direction = connectionVector / _length;
        RecalculateSegments(direction);
    }

    private void RecalculateSegments(Vector3 direction)
    {
        _segments = new GameObject[_segmentCount];

        for (var i = 0; i < _segmentCount; i++)
        {
            var segmentPosition = transform.position + 2f * segmentSize * i * -direction;
            var segment = Instantiate(segmentPrefab, segmentPosition, Quaternion.FromToRotation(Vector3.up, direction));
            segment.transform.parent = transform;
            var segmentJoint = segment.GetComponent<HingeJoint>();
            segmentJoint.connectedBody = i == 0 ? GetComponent<Rigidbody>() : _segments[i - 1].GetComponent<Rigidbody>();
            segment.GetComponent<HingeJointSegment>().Initialize(materials[_index]);
            _segments[i] = segment;
        }

        _anchor.transform.parent = transform;
        _anchor.GetComponent<HingeJoint>().connectedBody = _segments[_segmentCount - 1].GetComponent<Rigidbody>();
    }
}
