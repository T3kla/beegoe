using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    int health;
    bool enemiesRemaining;
    int currentRound;

    [SerializeField] GameObject GrassRings;

    public void AdvanceRound() // called when enemy killed and !enemiesRemaining
    {
        CalculateProgress();


        //PopWeaponUpgrade();
        //Inter Rounds Pause
        ShowRingsByProgress();
        StartRound();
        currentRound++;
    }

    void CalculateProgress()
    {
        health += 20;
    }

    void ShowRingsByProgress()
    {

    }

    public void StartRound()
    {
        //currentRound as difficulty multiplier
    }
}
