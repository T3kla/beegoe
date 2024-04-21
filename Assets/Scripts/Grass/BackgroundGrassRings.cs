using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGrassRings : MonoBehaviour
{
    int progressPerRing = 25;

    [SerializeField] GameObject[] aliveRings;
    [SerializeField] GameObject[] deadRings;


    [SerializeField] int ring;
    private void Update()
    {
        SetActiveRings(ring);
    }

    public void GetActiveRingsByProgress(int progress)
    {
        SetActiveRings(progress / progressPerRing);
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
