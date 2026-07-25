using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StringingController : MonoBehaviour
{
    [SerializeField] private GameObject springJointPrefab;
    [SerializeField] private GameObject hingeJointPrefab;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private GameObject hingeJointAnchorPrefab;
    
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private LineRenderer _lineRenderer;
    
    private Camera _mainCamera;
    
    private GameObject _pointer;
    private readonly SpringJointController[] _springJoints = new SpringJointController[4];
    private readonly HingeJointController[] _hingeJoints = new HingeJointController[4];

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
        
        var positions = new List<Vector3>();

        foreach (var t in _springJoints)
        {
            if (t == null)
            {
                continue;
            }
            
            positions.Add(transform.position);
            positions.Add(t.transform.position);
            positions.Add(transform.position);
        }

        if (positions.Count == 0)
        {
            _lineRenderer.enabled = false;
        }
        else
        {
            _lineRenderer.positionCount = positions.Count;
            _lineRenderer.SetPositions(positions.ToArray());
            // _lineRenderer.enabled = true;
        }
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        for (var i = 0; i < _springJoints.Length; i++)
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

        var joint = _springJoints[index];
        
        if (context.started)
        {
            if (joint != null || _pointer == null)
            {
                return;
            }
            
            var springJointGameObject = Instantiate(springJointPrefab, _pointer.transform.position, Quaternion.identity);
            var newSpringJoint = springJointGameObject.GetComponent<SpringJointController>();
            newSpringJoint.Initialize(_rigidbody, index);
            _springJoints[index] = newSpringJoint;
            
            var hingeJointGameObject = Instantiate(hingeJointPrefab, _pointer.transform.position, Quaternion.identity);
            var newHingeJoint = hingeJointGameObject.GetComponent<HingeJointController>();
            var hingeJointAnchorGameObject = Instantiate(hingeJointAnchorPrefab, transform.position, Quaternion.identity);
            var hingeJointAnchor = hingeJointAnchorGameObject.GetComponent<HingeJointAnchor>();
            hingeJointAnchor.Initialize(transform);
            newHingeJoint.Initialize(hingeJointAnchor, index);
            _hingeJoints[index] = newHingeJoint;
        }
        else if (context.canceled && joint != null)
        {
            Destroy(joint.gameObject);
            _springJoints[index] = null;
            
            Destroy(_hingeJoints[index].gameObject);
            _hingeJoints[index] = null;
        }
    }
}
