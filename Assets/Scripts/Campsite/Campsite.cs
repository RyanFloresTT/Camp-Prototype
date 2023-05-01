using System;
using UnityEngine;

public class Campsite : MonoBehaviour
{
    [SerializeField] private Detection detection;
    [SerializeField] private float spawnRadius = 5.0f;

    public static event EventHandler<Objective> PlayerAcceptedNewObjective;

    private ObjectiveGenerator objectiveGenerator = new();
    private Objective objective;
    private MainCameraController mainCameraController;
    private Vector2 campsitePosition;

    private void Start()
    {
        detection.ObjectEnteredRange += Handle_ObjectEnteredRange;
        detection.ObjectExitedRange += Detection_ObjectExitedRange;

        objective = objectiveGenerator.GenerateNewObjective();
        mainCameraController = MainCameraController.Instance;
        campsitePosition = new Vector2(transform.position.x, transform.position.y);

        SpawnEnemies();
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

    private void SpawnEnemies()
    {
        // TODO : Turn this into an Object Pool
        var enemy = objective.Enemy;
        for (int i = 0; i < objective.EnemyCount; i++)
        {
            var spawnedEnemy = Instantiate(enemy);
            var randomCircle = (UnityEngine.Random.insideUnitCircle * spawnRadius);
            spawnedEnemy.transform.position = campsitePosition + randomCircle;
        }
    }
}
