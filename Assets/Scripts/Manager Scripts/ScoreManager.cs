using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public Text scoreText;

    
    void Awake()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        AddScore(0);
    }
    void Update()
    {
        if(scoreText == null)
        {
            scoreText = GameObject.Find("Score").GetComponent<Text>();
            scoreText.text = score.ToString();
        }
    }


    public void AddScore(int amount)
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", score);

        scoreText.text = score.ToString();


    }
    public void ResetScore()
    {
        score = 0;
    }
}
