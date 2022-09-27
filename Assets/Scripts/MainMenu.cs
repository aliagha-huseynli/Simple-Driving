using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AndroidNotificationHandler _androidNotificationHandler;

    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _energyText;

    [SerializeField] private int _maxEnergy;
    [SerializeField] private int _energyRechargeDuration; // A Minute

    private DateTime _energyReady;
    private DateTime _energyRecharge;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private string _energyReadyString;

    private int _highScore;
    private int _energy;



    // Start
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt(Score.HighScoreKey, 0);
        _highScoreText.text += $"High Score: {_highScore}";

        _energy = PlayerPrefs.GetInt(EnergyKey, _maxEnergy);

        if (_energy == 0)
        {
            _energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (_energyReadyString == string.Empty) { return; } // For Error

            _energyReady = DateTime.Parse(_energyReadyString);

            if (DateTime.Now > _energyReady)
            {
                _energy = _maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, _energy);
            }
        }

        _energyText.text = $"PLAY ({_energy})"; // Display on Button

    }

    // Play
    public void Play()
    {
        if (_energy < 1) { return; }

        _energy--;

        PlayerPrefs.SetInt(EnergyKey, _energy);

        if (_energy == 0)
        {
            _energyRecharge = DateTime.Now.AddMinutes(_energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, _energyRecharge.ToString());

#if UNITY_ANDROID
            _androidNotificationHandler.ScheduleNotification(_energyRecharge);
#endif

        }

        SceneManager.LoadScene(1);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _energy = 5;
            PlayerPrefs.SetInt(EnergyReadyKey, 5);
            _energyText.text = $"PLAY ({_energy})";
            Debug.Log("Energy Cheat Activated!");
        }
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
