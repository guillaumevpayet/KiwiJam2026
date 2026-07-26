using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class StringingController : MonoBehaviour
{
    [SerializeField] private GameObject jointPrefab;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private AudioClip bounce;
    [SerializeField] private AudioClip snap;
    [SerializeField] private AudioClip splat;
    
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private LineRenderer _lineRenderer;
    
    private Camera _mainCamera;
    
    private GameObject _pointer;
    private readonly JointController[] _joints = new JointController[4];

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

        if (!Physics.SphereCast(ray.origin, 1, ray.direction, out var hitInfo, Mathf.Infinity, LayerMask.GetMask("Climbable")))
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
        
        List<Vector3> positions = new List<Vector3>();

        for (var i = 0; i < _joints.Length; i++)
        {
            if (_joints[i] != null)
            {
                positions.Add(transform.position);
                positions.Add(_joints[i].transform.position);
                positions.Add(transform.position);
            }
        }

        if (positions.Count == 0)
        {
            _lineRenderer.enabled = false;
        }
        else
        {
            _lineRenderer.positionCount = positions.Count;
            _lineRenderer.SetPositions(positions.ToArray());
            _lineRenderer.enabled = true;
        }
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        for (var i = 0; i < _joints.Length; i++)
        {
            HandleStringInput(context, i);
        }
    }

    private void HandleStringInput(InputAction.CallbackContext context, int index)
    {
        var actionName = $"String {index + 1}";
        
        if (context.action.name != actionName)
        {
            return;
        }

        var joint = _joints[index];
        
        if (context.started)
        {
            if (joint != null || _pointer == null)
            {
                return;
            }
            
            var jointGameObject = Instantiate(jointPrefab, _pointer.transform.position, Quaternion.identity);
            var newJoint = jointGameObject.GetComponent<JointController>();
            newJoint.Initialize(_rigidbody, index);
            _joints[index] = newJoint;
        }
        else if (context.canceled && joint != null)
        {
            Destroy(joint.gameObject);
            _joints[index] = null;
        }
    }
}
