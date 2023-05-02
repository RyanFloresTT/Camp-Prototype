using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject objectiveUIHolder;
    [SerializeField] private GameObject abandonWarningUI;
    [SerializeField] private TextMeshProUGUI objName;
    [SerializeField] private TextMeshProUGUI objDesc;
    [SerializeField] private TextMeshProUGUI objBonusTimer;
    [SerializeField] private GameObject enemy;

    public static ObjectiveManager Instance { get; private set; }
    public Objective CurrentObjective { get; private set; }
    private float bonusTimer;

    private void Awake()
    {
        Instance = this;
        CurrentObjective = null;
    }

    private void Start()
    {
        Campsite.PlayerAcceptedNewObjective += Handle_PlayerAcceptedNewObjective;
        Campsite.PlayerAboutToAbandon += Handle_PlayerAboutToAbandon;
        Campsite.PlayerAbondonedObjective += Handle_PlayerAbandonedObjective;
        TurnUIElementsOff(objectiveUIHolder);
        TurnUIElementsOff(abandonWarningUI);
    }

    private void Handle_PlayerAbandonedObjective(object sender, System.EventArgs e)
    {
        TurnUIElementsOff(objectiveUIHolder);
        CurrentObjective = null;
    }

    private void Handle_PlayerAboutToAbandon(object sender, bool willAbandon)
    {
        if (!willAbandon)
        {
            TurnUIElementsOff(abandonWarningUI);
        } else
        {
            TurnUIElementsOn(abandonWarningUI);
        }

    }

    private void Handle_PlayerAcceptedNewObjective(object sender, Objective e)
    {
        if (CurrentObjective != null) return;
        ActivateObjective(e);
        TurnUIElementsOn(objectiveUIHolder);
    }

    private void ActivateObjective(Objective objective)
    {
        CurrentObjective = objective;
        UpdateUIElements();
        StartBonusTimerCountdown();
    }

    private void TurnUIElementsOn(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    private void TurnUIElementsOff(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void UpdateUIElements()
    {
        objName.text = CurrentObjective.Name;
        objDesc.text = CurrentObjective.Description;
    }

    private void StartBonusTimerCountdown()
    {
        bonusTimer = CurrentObjective.BonusTimeLimitInSec;
    }

    private void Update()
    {
        if (CurrentObjective != null)
        {
            CountdownBonusTimer();
        }
    }

    private void CountdownBonusTimer()
    {
        bonusTimer -= Time.deltaTime;
        objBonusTimer.text = Mathf.Ceil(bonusTimer).ToString();
    }

    public GameObject GetEnemy() => enemy;
}
