using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreMultiplier;

    private float _score;



    void Update()
    {
        _score += Time.deltaTime * _scoreMultiplier;
        _scoreText.text = Mathf.FloorToInt(_score).ToString();
    }
}
