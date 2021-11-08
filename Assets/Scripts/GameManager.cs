using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Related")]
    private int score = 0;
    [SerializeField] Text scoreText;
    [SerializeField] Text bombText;
    [SerializeField] Button restartButton;
    [SerializeField] Text levelUpText;
    [SerializeField] float levelUpInterval = 20f;

    public int bombCount = 0;

    public bool isHeroAlive = true;

    public BaseSpawner enemySpawner;
    public BaseSpawner itemSpawner;

    #region Singleton

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    #endregion

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this);
    }

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();

        ticksBeforeLevelUp = levelUpInterval;
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    private void Update()
    {
        UpdateUI();

        LevelUpTimer();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        if (bombCount > 0)
        {
            bombText.text = $"Bomb: {bombCount}";
        }
        else
        {
            bombText.text = "";
        }
    }

    public void ReadyForRestart()
    {
        restartButton.gameObject.SetActive(true);
        Time.timeScale = 0.1f;
        Cursor.visible = true;
        isHeroAlive = false;
    }

    public void Restart()
    {
        //restartButton.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    #region Level Up

    private int currentLevel = 0;
    private float ticksBeforeLevelUp;

    private void LevelUpTimer()
    {
        ticksBeforeLevelUp -= Time.unscaledDeltaTime;
        if (ticksBeforeLevelUp <= 0)
        {
            LevelUp();
            ticksBeforeLevelUp += levelUpInterval;
        }
    }

    public void LevelUp()
    {
        StartCoroutine(ShowLevelUp(++currentLevel));
        LevelUpLogic(currentLevel);
    }

    private void LevelUpLogic(int level)
    {
        switch (level)
        {
            case 1:
                Time.timeScale = 1.15f;
                break;
            case 2:
                enemySpawner.spawnInterval = 1.35f;
                break;
            case 3:
                Time.timeScale = 1.25f;
                enemySpawner.SetWeights(5, 4, 1);
                break;
            case 4:
                enemySpawner.spawnInterval = 1.2f;
                break;
            case 5:
                Time.timeScale = 1.35f;
                enemySpawner.SetWeights(4, 4, 2);
                break;
            case 8:
                enemySpawner.SetWeights(3, 4, 3);
                break;
            default:
                Time.timeScale += 0.15f;
                break;
        }
    }

    public IEnumerator ShowLevelUp(int level)
    {
        levelUpText.gameObject.SetActive(true);

        levelUpText.text = $"Level {level}";
        levelUpText.GetComponent<Animator>().SetTrigger("Show");

        yield return new WaitForSeconds(3f);
        levelUpText.gameObject.SetActive(false);
    }

    #endregion
}
