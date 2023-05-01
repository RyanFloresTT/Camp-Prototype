using UnityEngine;
using System.IO;
using System.Linq;

public class ObjectiveGenerator
{
    private string namesfileName = "names.txt";
    private string descriptionsfileName = "descriptions.txt";
    public Objective GenerateNewObjective()
    {
        return new Objective(GetBonusTimer(), GetName(), GetDescription(), GetEnemies(), GetEnemyCount(), GetXPReward());
    }

    private int GetBonusTimer() => Random.Range(60, 240);
    private string GetName()
    {
        var filePath = Application.dataPath + "/CampsiteInfo/" + namesfileName;
        var namesArray = File.ReadAllLines(filePath);
        var randIndex = Random.Range(0, namesArray.Length);
        return namesArray[randIndex];
    }

    private string GetDescription()
    {
        var filePath = Application.dataPath + "/CampsiteInfo/" + descriptionsfileName;
        var descriptionArray = File.ReadAllLines(filePath);
        var randIndex = Random.Range(0, descriptionArray.Length);
        return descriptionArray[randIndex];
    }

    private GameObject GetEnemies() => ObjectiveManager.Instance.GetEnemy();

    private int GetEnemyCount() => Random.Range(1, 3);

    private int GetXPReward() => Random.Range(0, 100);
}
