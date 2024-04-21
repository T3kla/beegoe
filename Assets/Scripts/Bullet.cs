using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static List<Transform> Each = new ();

    private void OnEnable()
    {
        StartCoroutine(Lifetime());
    }

    public static Transform Spawn(Transform prefab, Vector3 position, Quaternion rotation)
    {
        foreach (var bullet in Each)
        {
            if (bullet.gameObject.activeSelf) continue;
            
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.gameObject.SetActive(true);
            return bullet.transform;
        }

        var obj = Instantiate(prefab, position, rotation).transform;
        Each.Add(obj);
        return obj;
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.collider.GetComponent<Wasp>()?.Hit();
        
        gameObject.SetActive(false);
    }
}
