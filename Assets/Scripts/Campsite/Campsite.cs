using System;
using System.Collections;
using UnityEngine;

public class Campsite : MonoBehaviour
{
    [SerializeField] private Detection detection;
    [SerializeField] private float spawnRadius = 5.0f;

    public static event EventHandler<Objective> PlayerAcceptedNewObjective;
    public static event EventHandler<bool> PlayerAboutToAbandon;
    public static event EventHandler PlayerAbondonedObjective;

    private ObjectiveGenerator objectiveGenerator = new();
    private Objective objective;
    private MainCameraController mainCameraController;
    private Vector2 campsitePosition;
    private bool willAbandon;
    private Objective playerCurrentObjective;
    private int abandonDelay = 3;

    private void Start()
    {
        detection.ObjectEnteredRange += Handle_ObjectEnteredRange;
        detection.ObjectExitedRange += Handle_ObjectExitedRange;

        objective = objectiveGenerator.GenerateNewObjective();
        mainCameraController = MainCameraController.Instance;
        campsitePosition = new Vector2(transform.position.x, transform.position.y);

        SpawnEnemies();
    }

    private void Handle_ObjectEnteredRange(object sender, GameObject e)
    {
        playerCurrentObjective = ObjectiveManager.Instance.CurrentObjective;
        //mainCameraController.PlayCampsiteCinematic(transform);
        if (playerCurrentObjective == null)
        {
            PlayerAcceptedNewObjective?.Invoke(this, objective);
        } else
        {
            willAbandon = false;
            Debug.Log(willAbandon);
            PlayerAboutToAbandon?.Invoke(this, willAbandon);
        }
    }

    private void Handle_ObjectExitedRange(object sender, GameObject e)
    {
        // Remove Objective Information from Player After a Brief Delay
        StartCoroutine(AbandonObjectiveAfterDelay(abandonDelay));
    }

    private IEnumerator AbandonObjectiveAfterDelay(int delay)
    {
        willAbandon = true;
        Debug.Log(willAbandon);
        PlayerAboutToAbandon?.Invoke(this, willAbandon);
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log(willAbandon);
        if (willAbandon) PlayerAbondonedObjective?.Invoke(this, EventArgs.Empty);
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
