using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;

    private int _highScore;
    private Score _score;


    // Start
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt(Score.HighScoreKey, 0);
        _highScoreText.text += $"High Score: {_highScore}";
    }

    // Play
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    // Reset High Score
    public void ResetHighScore()
    {
        _highScore = 0;
        PlayerPrefs.SetInt(Score.HighScoreKey, 0);
        _highScoreText.text = $"High Score: {_highScore}";
    }

    // Quit
    public void Quit()
    {
        Application.Quit();
    }
}
