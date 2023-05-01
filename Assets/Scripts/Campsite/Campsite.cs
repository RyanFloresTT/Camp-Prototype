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
    private MainCameraController mainCameraController;

    private void Start()
    {
        detection.ObjectEnteredRange += Handle_ObjectEnteredRange;
        detection.ObjectExitedRange += Detection_ObjectExitedRange;

        objective = objectiveGenerator.GenerateNewObjective();
        mainCameraController = MainCameraController.Instance;
    }

    private void Handle_ObjectEnteredRange(object sender, GameObject e)
    {
        //mainCameraController.PlayCampsiteCinematic(transform);
        PlayerAcceptedNewObjective?.Invoke(this, objective);
    }

    private void Detection_ObjectExitedRange(object sender, GameObject e)
    {
        // Remove Objective Information from Player After a Brief Delay
    }
}
