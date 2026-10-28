using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [SerializeField]private float _movementSpeed = 10f;
    [SerializeField]private float _jumpHeight = 2.0f;
    [SerializeField]private Transform _groundSensor;
    [SerializeField]private float _sensorSize = 0f;

    private InputAction _jumpAction;
    private InputAction _moveAction;
    private Vector2 _moveInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
 //_moveAction = InputSystem.actions.FindAction("Move");
// _jumpAction = InputSystem.actions.FindAction("Jump");


    // Update is called once per frame
     void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();

        if(_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if(_animator != null) { _animator.SetBool("IsRunning", true); }
        }
        else if(_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if(_animator != null) { _animator.SetBool("IsRunning", true); }
        }
        else
        {
            if(_animator != null) { _animator.SetBool("IsRunning", false); }
        }

   //     if(_jumpAction.WasPressedThisFrame() && IsGrounded())
                if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        if(_animator != null) { _animator.SetBool("IsJumping", !IsGrounded()); }
    }

    void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = new Vector2(_moveInput.x * _movementSpeed, _rigidbody2D.linearVelocity.y);
    }

    void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * Mathf.Sqrt(_jumpHeight * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_groundSensor.position, _sensorSize);
        foreach (Collider2D item in colliders2D)
        {
            if(item.gameObject.layer == 6)
            {
                return true;
            }
        }
        return false;
    }

  
}

