using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDeadgrassRings : MonoBehaviour
{
    int progress;
    int progressPerRing = 25;


    [SerializeField] GameObject ring_1;
    [SerializeField] GameObject ring_2;
    [SerializeField] GameObject ring_3;

    public void GetActiveRingsByProgress()
    {
        SetActiveRings(progress / progressPerRing);
    }

    public void SetActiveRings(int ring)
    {
        ring_3.SetActive(ring >= 3);
        ring_2.SetActive(ring >= 2);
        ring_1.SetActive(ring >= 1);
    }
}
