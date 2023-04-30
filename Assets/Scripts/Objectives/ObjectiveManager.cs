using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objName;
    [SerializeField] private TextMeshProUGUI objDesc;
    [SerializeField] private TextMeshProUGUI objBonusTimer;

    private Objective currentObjective;
    private float bonusTimer;
    public static ObjectiveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Campsite.PlayerAcceptedNewObjective += Handle_PlayerAcceptedNewObjective;
    }

    private void Handle_PlayerAcceptedNewObjective(object sender, Objective e)
    {
        ActivateObjective(e);
    }

    private void ActivateObjective(Objective objective)
    {
        currentObjective = objective;
        UpdateUIElements();
        StartBonusTimerCountdown();
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
        CountdownBonusTimer();
    }

    private void CountdownBonusTimer()
    {
        bonusTimer -= Time.deltaTime;
        objBonusTimer.text = bonusTimer.ToString();
    }
}
