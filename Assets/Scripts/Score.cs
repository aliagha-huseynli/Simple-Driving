using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreMultiplier;

    public const string HighScoreKey = "HighScore";

    public float _score;

    private int _currentHighScore;


// Update
    private void Update()
    {
        _score += Time.deltaTime * _scoreMultiplier;
        _scoreText.text = Mathf.FloorToInt(_score).ToString();
    }

// OnDestroy
    private void OnDestroy()
    {
        _currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (_score > _currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(_score));
        }
    }
}
