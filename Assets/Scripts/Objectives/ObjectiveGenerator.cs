using System;
using UnityEngine;

public class ObjectiveGenerator
{
    public Objective GenerateNewObjective()
    {
        return new Objective(100, "Name", "Description", GameObject.FindGameObjectsWithTag("MainCamera"), 3);
    }
}
