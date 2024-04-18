using UnityEngine;
using UnityEngine.Serialization;

public class Camera : MonoBehaviour
{
    [FormerlySerializedAs("smoothTime")] [SerializeField] private float smoothness = 0.3f;

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
    }

    private void LateUpdate()
    {
        var targetPosition = _target.position + _offset;
        var smooth = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothness);
        transform.position = smooth;
    }
}
