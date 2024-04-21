using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    int progress;
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
        progress += 20;
    }

    void ShowRingsByProgress()
    {

    }

    public void StartRound()
    {
        //currentRound as difficulty multiplier
    }
}
