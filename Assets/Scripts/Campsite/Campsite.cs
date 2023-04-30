using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campsite : MonoBehaviour
{
    [SerializeField] Detection detection;

    public static event EventHandler<Objective> PlayerAcceptedNewObjective;

    private ObjectiveGenerator objectiveGenerator = new();
    private Objective objective;

    private void Start()
    {
        detection.ObjectEnteredRange += Handle_ObjectEnteredRange;
        detection.ObjectExitedRange += Detection_ObjectExitedRange;

        objective = objectiveGenerator.GenerateNewObjective();
    }

    private void Handle_ObjectEnteredRange(object sender, GameObject e)
    {
        // Play Cinematic to Show off Camp
        // Give Objective Information to Player
        PlayerAcceptedNewObjective?.Invoke(this, objective);
    }

    private void Detection_ObjectExitedRange(object sender, GameObject e)
    {
        // Remove Objective Information from Player (Might have to tweak the numbers on the range)
    }
}
