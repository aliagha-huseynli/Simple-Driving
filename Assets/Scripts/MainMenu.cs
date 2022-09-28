using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AndroidNotificationHandler _androidNotificationHandler;
    [SerializeField] private IOSNotificationHandler _iosdNotificationHandler;

    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _energyText;

    [SerializeField] private Button _playButton;

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
        OnApplicationFocus(true);
        _highScoreText.text += $"High Score: {_highScore}";
    }

    // OnApplicationFocus
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }

        CancelInvoke();

        _highScore = PlayerPrefs.GetInt(Score.HighScoreKey, 0);               

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
            else
            {
                _playButton.interactable = false; // Turn Off Play Button
                Invoke(nameof(EnergyRecharged), (_energyReady - DateTime.Now).Seconds);
            }
        }

        _energyText.text = $"PLAY ({_energy})"; // Display on Button

    }

    //EnergyRecharged
    private void EnergyRecharged()
    {
        _playButton.interactable = true; // Turn On Play Button
        _energy = _maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, _energy);
        _energyText.text = $"PLAY ({_energy})";
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

            // Mobile Platforms
#if UNITY_ANDROID
            _androidNotificationHandler.ScheduleNotification(_energyRecharge);
#elif UNITY_IOS
            _iosdNotificationHandler.ScheduleNotification(_energyRechargeDuration);
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
