using TMPro;
using UnityEngine;

/// <summary>
/// AUTHOR: @Daniel K.
/// Last modified: 12 Dec. 2022 by @Daniel K.
/// </summary>
/// 
public class Timer : MonoBehaviour
{
    /* EXPOSED FIELDS */
    [SerializeField] private TextMeshProUGUI timeText;
    
    /* HIDDEN FIELDS */
    private static float _timer;
    private static bool _isTimeMeasured = true;
    
    void Start()
    {
        _timer = Time.timeSinceLevelLoad;
    }
    
    void FixedUpdate()
    {
        SampleTime();
        DisplayTime();
    }

    /* PRIVATE METHODS */
    private void SampleTime()
    {
        if (!_isTimeMeasured) return;
        // timer = Time.time;
        _timer = Time.timeSinceLevelLoad;
    }

    private void DisplayTime()
    {
        timeText.text = GetTimer();
    }

    /* PUBLIC METHODS */
    public static void TurnOffTimer()
    {
        _isTimeMeasured = false;
    }

    public static string GetTimer()
    {
        int minutes = Mathf.FloorToInt(_timer / 60F);
        int seconds = Mathf.FloorToInt(_timer - minutes * 60);
        int mSeconds = Mathf.FloorToInt(_timer*100.0f % 100.0f);
        string formattedTime = $"{minutes:0}:{seconds:00}:{mSeconds:00}";
        return formattedTime;
    }
}
