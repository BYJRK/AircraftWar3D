using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;
    [SerializeField] float startPoint;

    [SerializeField] GameObject[] enemies;
    [SerializeField] float[] randomWeights;

    [SerializeField] GameObject[] powerups;

    private float spawnInterval = 1.6f;
    private int level = 0;

    private float nextPowerUpTick = 10;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            var x = Random.Range(leftBound, rightBound);
            var i = GetRandomIndex(randomWeights);
            Instantiate(enemies[i], new Vector3(x, 0, startPoint), Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval * Random.Range(0.25f, 1.25f));
        }
    }

    public void LevelUp()
    {
        level++;

        switch (level)
        {
            case 1:
                Time.timeScale = 1.1f;
                break;
            case 2:
                spawnInterval = 1.35f;
                break;
            case 3:
                Time.timeScale = 1.25f;
                randomWeights = new float[] { 5, 4, 1 };
                break;
            case 4:
                spawnInterval = 1.2f;
                break;
            case 5:
                Time.timeScale = 1.35f;
                randomWeights = new float[] { 4, 4, 2 };
                break;
            case 8:
                randomWeights = new float[] { 3, 4, 3 };
                break;
            default:
                Time.timeScale += 0.15f;
                break;
        }
        GameManager.Instance.ShowLevelUp(level);
    }

    private int GetRandomIndex(float[] weights)
    {
        var r = Random.Range(0, weights.Sum());
        for (int i = 0; i < weights.Length; i++)
        {
            r -= weights[i];
            if (r <= 0)
                return i;
        }
        return 0;
    }

    private float remainTimeToLevelUp = 20f;
    private float totalTime = 0f;

    private void Update()
    {
        nextPowerUpTick -= Time.deltaTime;

        // generate power ups
        if (nextPowerUpTick <= 0)
        {
            Instantiate(powerups[Random.Range(0, powerups.Length)],
                new Vector3(Random.Range(leftBound, rightBound), 0, startPoint),
                Quaternion.identity);

            nextPowerUpTick = 20;
        }

        // level up logic
        if (!GameManager.Instance.isHeroAlive)
            return;
        totalTime += Time.unscaledDeltaTime;
        if (totalTime > remainTimeToLevelUp)
        {
            LevelUp();
            remainTimeToLevelUp += 20f;
        }
    }
}
