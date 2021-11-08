using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : BaseSpawner
{
    [SerializeField]
    [Range(0, 1)]
    float chanceToSpawnDouble = 0.1f;

    protected override void TriggerNextSpawn()
    {
        InstantiateNewObject();

        // a 20% of chance to instantly instantiate another one
        if (Random.Range(0, 1) < chanceToSpawnDouble)
            InstantiateNewObject();
    }
}
