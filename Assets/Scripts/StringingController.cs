using UnityEngine;
using UnityEngine.InputSystem;

public class StringingController : MonoBehaviour
{
    [SerializeField] private GameObject jointPrefab;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Material yellow;
    [SerializeField] private Material green;
    [SerializeField] private float elasticity = 1f;
    
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private GameObject _pointer;
    private SpringJoint _springJoint;
    private LineRenderer _lineRenderer;
    private Camera _mainCamera;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _playerInput.onActionTriggered += OnActionTriggered;
        _mainCamera = Camera.main;

        if (_mainCamera != null)
        {
            return;
        }
        
        Debug.LogError("Main camera not found");
    }

    private void Update()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        var ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, LayerMask.GetMask("Climbable")))
        {
            Destroy(_pointer);
            _pointer = null;
        }
        else
        {
            var pointedPosition = hitInfo.point;
            pointedPosition.z = 0f;
        
            if (_pointer == null)
            {
                _pointer = Instantiate(pointerPrefab, pointedPosition, Quaternion.identity);
            }
            else
            {
                _pointer.transform.position = pointedPosition;
            }
        }

        if (_springJoint == null)
        {
            return;
        }

        _springJoint.maxDistance = Mathf.Pow(0.9f, elasticity) * _springJoint.maxDistance;
        
        Vector3[] positions = { transform.position, _springJoint.transform.position };
        _lineRenderer.SetPositions(positions);
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name != "Attack")
        {
            return;
        }

        if (_pointer == null)
        {
            return;
        }

        _pointer.GetComponent<Renderer>().material = context.started ? green : yellow;
        
        if (context.started)
        {
            if (_springJoint != null)
            {
                return;
            }
            
            var joint = Instantiate(jointPrefab, _pointer.transform.position, Quaternion.identity);
            _springJoint = joint.GetComponent<SpringJoint>();
            _springJoint.connectedBody = _rigidbody;
            _springJoint.maxDistance = Vector3.Distance(transform.position, _pointer.transform.position);
            Vector3[] positions = { transform.position, _pointer.transform.position };
            _lineRenderer.SetPositions(positions);
            _lineRenderer.enabled = true;
        }
        else if (context.canceled && _springJoint != null)
        {
            Destroy(_springJoint.gameObject);
            _springJoint = null;
            _lineRenderer.enabled = false;
        }
    }
}
