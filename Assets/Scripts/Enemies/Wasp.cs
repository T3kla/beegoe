using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class Wasp : MonoBehaviour
    {
        public static List<Transform> Each = new();
        
        [SerializeField] private SpriteRenderer waspSpriteRenderer = null;

        [Header("Movement")]
        [SerializeField] private float maxSpeed = 20;
        [SerializeField] private float acceleration = 100;
        
        [Header("Health")]
        [SerializeField] private int health = 5;
        [SerializeField] private Sprite hitSprite = null;
        [SerializeField] private Sprite normalSprite = null;
        
        [Header("Attack")]
        [SerializeField] private float attackRate = 0.5f;

        private int _health = 0;
        private Rigidbody2D _rb = null;
        private bool _attacking = false;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _health = health;
        }
        
        protected void Move(Vector3 target)
        {
            var dir = target - transform.position;
            
            if (dir.magnitude < 3.5f && _attacking == false)
                StartCoroutine(Attack());
            
            var norm = dir.normalized;

            _rb.velocity += new Vector2(norm.x, norm.y) * (acceleration * Time.deltaTime);

            if (_rb.velocity.magnitude > maxSpeed)
                _rb.velocity = _rb.velocity.normalized * maxSpeed;

            waspSpriteRenderer.flipX = norm.x < 0;
        }
        
        public void Hit()
        {
            if (gameObject.activeSelf == false) return;
            
            _health--;
            StartCoroutine(Hurt());

            if (_health <= 0)
                gameObject.SetActive(false);
        }
        
        private IEnumerator Hurt()
        {
            waspSpriteRenderer.sprite = hitSprite;
            yield return new WaitForSeconds(0.1f);
            waspSpriteRenderer.sprite = normalSprite;
        }

        public static Transform Spawn(Transform prefab, Vector3 position, Quaternion rotation)
        {
            Wasp script = null;
            Transform obj = null;
            
            foreach (var wasp in Each)
            {
                if (wasp.gameObject.activeSelf) continue;

                wasp.transform.position = position;
                wasp.transform.rotation = rotation;
                wasp.gameObject.SetActive(true);
                obj = wasp.transform;
                break;
            }

            if (obj == null)
            {
                obj = Instantiate(prefab, position, rotation).transform;
                Each.Add(obj);
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            script = obj.GetComponent<Wasp>();
            script._health = script.health;
            script.waspSpriteRenderer.sprite = script.normalSprite;

            return obj;
        }
        private IEnumerator Attack()
        {
            _attacking = true;
            
            
            yield return new WaitForSeconds(attackRate);
            Home.Instance?.Hit();
            yield return new WaitForSeconds(attackRate);
            
            _attacking = false;
        }
    }
}
