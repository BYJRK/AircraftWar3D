using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] Boundary bound;

    [SerializeField] SpawnObject[] objects;

    [Min(0)]
    public float spawnInterval;
    [Range(0, 1)]
    public float floatingFactor = 0f;

    [Tooltip("this can be considered as a time offset before the first spawn since start")]
    [Min(0)]
    public float ticksBeforeNextSpawn;

    protected void Start()
    {
        if (ticksBeforeNextSpawn == 0)
            ticksBeforeNextSpawn = spawnInterval * RandomHelper.FloatingFactor(floatingFactor);
    }

    protected virtual void TriggerNextSpawn()
    {
        InstantiateNewObject();
    }

    protected void InstantiateNewObject()
    {
        var idx = RandomHelper.GetRandomIndex(objects.Select(o => o.randomWeight).ToArray());

        var x = Random.Range(bound.leftBound, bound.rightBound);
        var obj = Instantiate(objects[idx].prefab, new Vector3(x, 0, bound.TopBound), Quaternion.identity);
    }

    protected void Update()
    {
        // spawn next object
        ticksBeforeNextSpawn -= Time.deltaTime;
        if (ticksBeforeNextSpawn <= 0)
        {
            TriggerNextSpawn();
            ticksBeforeNextSpawn += spawnInterval * RandomHelper.FloatingFactor(floatingFactor);
        }
    }

    public void SetWeights(params float[] weights)
    {
        for (int i = 0; i < Mathf.Min(weights.Length, objects.Length); i++)
        {
            objects[i].randomWeight = weights[i];
        }
    }

    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;
        public float randomWeight = 1f;
    }
}
