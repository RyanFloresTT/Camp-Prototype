using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject objectiveUIHolder;
    [SerializeField] private TextMeshProUGUI objName;
    [SerializeField] private TextMeshProUGUI objDesc;
    [SerializeField] private TextMeshProUGUI objBonusTimer;
    [SerializeField] private GameObject enemy;

    private Objective currentObjective;
    private float bonusTimer;
    public static ObjectiveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        currentObjective = null;
    }

    private void Start()
    {
        Campsite.PlayerAcceptedNewObjective += Handle_PlayerAcceptedNewObjective;
        TurnUIElementsOff();
    }

    private void Handle_PlayerAcceptedNewObjective(object sender, Objective e)
    {
        if (currentObjective != null) return;
        ActivateObjective(e);
        TurnUIElementsOn();
    }

    private void ActivateObjective(Objective objective)
    {
        currentObjective = objective;
        UpdateUIElements();
        StartBonusTimerCountdown();
    }

    private void TurnUIElementsOn()
    {
        objectiveUIHolder.SetActive(true);
    }
    private void TurnUIElementsOff()
    {
        objectiveUIHolder.SetActive(false);
    }

    private void UpdateUIElements()
    {
        objName.text = currentObjective.Name;
        objDesc.text = currentObjective.Description;
    }

    private void StartBonusTimerCountdown()
    {
        bonusTimer = currentObjective.BonusTimeLimitInSec;
    }

    private void Update()
    {
        if (currentObjective != null)
        {
            CountdownBonusTimer();
        }
    }

    private void CountdownBonusTimer()
    {
        bonusTimer -= Time.deltaTime;
        objBonusTimer.text = bonusTimer.ToString();
    }

    public GameObject GetEnemy() => enemy;
}
