using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] Text scoreText;
    [SerializeField] Button restartButton;
    [SerializeField] Text levelUpText;

    Color levelUpColor;
    Color levelUpTransparentColor;

    public bool isHeroAlive = true;

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

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this);

        levelUpColor = levelUpText.color;
        levelUpTransparentColor = levelUpColor;
        levelUpTransparentColor.a = 0;
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
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

    public void ShowLevelUp(int level)
    {
        StartCoroutine(ShowLevelUpText(level));
    }

    IEnumerator ShowLevelUpText(int level)
    {
        levelUpText.gameObject.SetActive(true);
        levelUpText.color = levelUpColor;
        var color = levelUpText.color;

        levelUpText.text = $"Level {level}";

        yield return new WaitForSecondsRealtime(2f);

        while (color.a > 0.05)
        {
            color = Color.Lerp(color, levelUpTransparentColor, 0.05f);
            levelUpText.color = color;
            yield return null;
        }

        levelUpText.gameObject.SetActive(false);
    }
}
