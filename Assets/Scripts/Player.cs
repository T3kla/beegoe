using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Cam cam = null;
    
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float acceleration = 100;
    
    private Rigidbody2D _rb = null;
    private Vector2 _move = Vector2.zero;
    
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer beeSpriteRenderer = null;
    [SerializeField] private float smoothness = 0.01f;

    private Vector2 _look = Vector2.zero;
    
    [Header("Mini")]
    [SerializeField] private Transform bulletPrefab = null;
    [SerializeField] private float bulletSpeed = 1;
    [SerializeField] private SpriteRenderer miniSpriteRenderer = null;
    [SerializeField] private Transform miniPivot = null;
    [SerializeField] private SpriteRenderer miniMuzzleSpriteRenderer = null;
    [SerializeField] private Transform miniMuzzle = null;
    [SerializeField] private Sprite sprMini0 = null;
    [SerializeField] private Sprite sprMini1 = null;
    [SerializeField] private Sprite sprMiniMuzzle0 = null;
    [SerializeField] private Sprite sprMiniMuzzle1 = null;
    [SerializeField] private float miniTorqueAcceleration = 1;
    [SerializeField] private float miniTorqueDeceleration = 0.2f;
    [SerializeField] private float miniTorqueMax = 5;
    [SerializeField] private float miniSpriteChangeSpeed = 1;
    [SerializeField] private float miniOrbitRadius = 1;

    [Header("Shake")]
    [SerializeField] private float shakeMagnitude = 0.1f;
    
    private bool _charging = false;
    private bool _firing = false;
    private float _torque = 0;
    private float _revolutions = 0;
    
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
        _charging = value.Get<float>() > 0.5f;
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
        var beeScreenPos = cam.Camera.WorldToScreenPoint(transform.position);
        var beeToMouse = _look - new Vector2(beeScreenPos.x, beeScreenPos.y);
        
        var displacement = beeToMouse.normalized * miniOrbitRadius;
        var newPosition = new Vector3(displacement.x, displacement.y, 0);
        var newAngle = Mathf.Atan2(beeToMouse.y, beeToMouse.x) * Mathf.Rad2Deg;
        
        if (newAngle < 0)
            newAngle += 360;
        
        var prevAngle = miniPivot.rotation.eulerAngles.z;
        var smoothAngle = (newAngle - prevAngle) * smoothness * Time.deltaTime + prevAngle;

        miniPivot.SetLocalPositionAndRotation(newPosition, Quaternion.Euler(0, 0, newAngle));
        miniSpriteRenderer.flipY = smoothAngle is > 90 and < 270;
        beeSpriteRenderer.flipX = smoothAngle is > 90 and < 270;
    }

    private void Fire()
    {
        _torque += (_charging ? miniTorqueAcceleration : -miniTorqueDeceleration) * Time.deltaTime;
        _torque = Mathf.Clamp(_torque, 0, miniTorqueMax);

        _revolutions += _torque * Time.deltaTime;
        var mod = Mathf.FloorToInt(_revolutions % miniSpriteChangeSpeed);
        var prevSprite = miniSpriteRenderer.sprite;
        var newSprite = mod == 0 ? sprMini1 : sprMini0;
        miniSpriteRenderer.sprite = newSprite;
        
        if (_firing && prevSprite != newSprite)
        {
            var bullet = Bullet.Spawn(bulletPrefab, miniMuzzle.position, miniMuzzle.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = miniMuzzle.right * bulletSpeed;
            miniMuzzleSpriteRenderer.sprite = mod == 0 ? sprMiniMuzzle1 : sprMiniMuzzle0;
        }
        else if (!_firing)
        {
            miniMuzzleSpriteRenderer.sprite = null;
        }
        
        cam.Shake = _torque * shakeMagnitude;
    }
}
