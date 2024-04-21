using System;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private float smoothness = 0.3f;
    [NonSerialized] public float Shake = 0;
    [NonSerialized] public UnityEngine.Camera Camera = null;

    private Transform _target = null;
    private Vector3 _offset = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        if (transform.parent == null) return;
        
        var t = transform;
        _target = t.parent;
        _offset = t.position - _target.position;
        transform.SetParent(null);

        Camera = GetComponent<UnityEngine.Camera>();
    }

    private void LateUpdate()
    {
        var targetPosition = _target.position + _offset;
        var smooth = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothness);
        
        if (Shake > 0)
            smooth += UnityEngine.Random.insideUnitSphere * Shake;
        
        transform.position = smooth;
    }
}
