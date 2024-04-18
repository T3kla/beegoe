using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float acceleration = 100;

    private Rigidbody2D _rb = null;
    private Vector2 _move = Vector2.zero;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();


        _rb.velocity += _move * acceleration * Time.deltaTime;

        if (_rb.velocity.magnitude > maxSpeed)
            _rb.velocity = _rb.velocity.normalized * maxSpeed;
    }

    private void Update()
    {
        _rb.velocity += _move * (acceleration * Time.deltaTime);

        if (_rb.velocity.magnitude > maxSpeed)
            _rb.velocity = _rb.velocity.normalized * maxSpeed;
    }

    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
}
