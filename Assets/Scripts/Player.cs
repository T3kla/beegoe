using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float acceleration = 100;
    
    private Rigidbody2D _rb = null;
    private Vector2 _move = Vector2.zero;
    
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite = null;
    [SerializeField] private float smootness = 0.01f;

    private UnityEngine.Camera _cam = null;
    private Vector2 _look = Vector2.zero;
    
    [Header("Shoot")]
    [SerializeField] private Transform bulletPrefab = null;
    [SerializeField] private Transform bulletOrigin = null;
    [SerializeField] private float miniTorqueAcceleration = 10;
    [SerializeField] private float miniTorqueMax = 100;

    private bool _charging = false;
    private bool _firing = false;
    private float _charge = 0;
    
    // Input System

    private void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }
    
    private void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        _firing = value.Get<float>() > 0;
    }

    private void OnCharge(InputValue value)
    {
        _charging = value.Get<float>() > 0;
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
        Fire();
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

    private void Fire()
    {
        // if (_charging)
            // _charge += _charging ?  
    }
}
