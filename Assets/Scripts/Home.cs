using System.Collections;
using Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public static Home Instance { get; private set; }

    [Header("Home")]
    [SerializeField] private BackgroundGrassRings backgroundGrassRings = null;
    [SerializeField] private int health = 100;
    
    [Header("Spawn Basic wasp")]
    [SerializeField] private Transform waspBasicPrefab = null;
    [SerializeField] private float waspBasicSpawnRate = 1;
    [SerializeField] private float waspBasicRadius = 10;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnBasic());
    }

    private IEnumerator SpawnBasic()
    {
        while (true)
        {
            yield return new WaitForSeconds(waspBasicSpawnRate);
            Wasp.Spawn(waspBasicPrefab, UnityEngine.Random.insideUnitCircle.normalized * waspBasicRadius, Quaternion.identity);
        }
    }
    
    public void Hit()
    {
        health--;

        backgroundGrassRings.GetActiveRingsByHealth(health);

        if (health <= 0)
            SceneManager.LoadScene(0);
    }
}
