using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTracker : MonoBehaviour
{
    bool enemiesRemaining;
    int currentRound;

    public void StartRound()
    {
        //currentRound as difficulty multiplier
    }


    public void AdvanceRound() // called when enemy killed and !enemiesRemaining
    {
        //Call deadgrassrings
        //Pop weapon upgrade
        //Wait x seconds
        StartRound();
        currentRound++;
    }
}
