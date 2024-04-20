using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float acceleration = 100;
    
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite = null;
    [SerializeField] private float smootness = 0.01f;

    private Rigidbody2D _rb = null;
    private UnityEngine.Camera _cam = null;
    private Vector2 _move = Vector2.zero;
    private Vector2 _look = Vector2.zero;
    
    // Input System

    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
    
    private void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    //
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }  
    
    private void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        _rb.velocity += _move * (acceleration * Time.deltaTime);

        if (_rb.velocity.magnitude > maxSpeed)
            _rb.velocity = _rb.velocity.normalized * maxSpeed;
    }

    private void Look()
    {
        if (_cam == null)
        {
            _cam = UnityEngine.Camera.main;
            return;
        }
        
        var beeScreenPos = _cam.WorldToScreenPoint(transform.position);
        var beeToMouse = _look - new Vector2(beeScreenPos.x, beeScreenPos.y);
        
        var newAngle = Mathf.Atan2(beeToMouse.y, beeToMouse.x) * Mathf.Rad2Deg;
        
        if (newAngle < 0)
            newAngle += 360;
        
        var prevAngle = sprite.transform.rotation.eulerAngles.z;
        var smoothAngle = (newAngle - prevAngle) * smootness * Time.deltaTime + prevAngle;
        
        sprite.transform.rotation = Quaternion.Euler(0, 0, newAngle);
        sprite.flipY = smoothAngle is > 90 and < 270;
    }
}