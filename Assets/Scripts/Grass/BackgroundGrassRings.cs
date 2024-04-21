using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrassRings : MonoBehaviour
{
    int healthPerRing = 25;

    [SerializeField] GameObject[] aliveRings;
    [SerializeField] GameObject[] deadRings;


    //[SerializeField] int ring;
    //private void FixedUpdate()
    //{
    //    SetActiveRings(ring);
    //}

    public void GetActiveRingsByHealth(int health)    // pasar aqui vida
    {
        SetActiveRings(health / healthPerRing);
    }

    public void SetActiveRings(int ring)
    {
        for (int i = 0; i < 4; i++)
        {
            aliveRings[i].SetActive(ring > i);
            deadRings[i].SetActive(ring <= i);
        }
    }
}
