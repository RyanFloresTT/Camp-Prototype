using UnityEngine;

public class Objective
{
    public int BonusTimeLimitInSec { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public GameObject Enemy { get; private set; }
    public int EnemyCount { get; private set; }
    public int RewardXP { get; private set; }

    public Objective(int bonusTimeLimitInSec, string name, string description, GameObject enemy, int enemyCount, int rewardXP)
    {
        this.BonusTimeLimitInSec = bonusTimeLimitInSec;
        this.Name = name;
        this.Description = description;
        this.Enemy = enemy;
        this.EnemyCount = enemyCount;
        this.RewardXP = rewardXP;
    }

    public void OnObjectiveComplete()
    {
        // Villagers Populate (Move in From Beyond Camera)
        // Give Player rewardXP and other such future rewards
        // Generate a number of soldiers the player can choose from (1-3)
    }
}
