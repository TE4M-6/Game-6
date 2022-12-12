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
    private static float timer;
    private static bool isTimeMeasured = true;
    
    void Start()
    {
        // timer = Time.time;
        timer = Time.timeSinceLevelLoad;
    }
    
    void FixedUpdate()
    {
        SampleTime();
        DisplayTime();
    }

    private void DisplayTime()
    {
        timeText.text = GetTimer();
    }

    public static void TurnOffTimer()
    {
        isTimeMeasured = false;
    }

    public static string GetTimer()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        int mSeconds = Mathf.FloorToInt(timer*100.0f % 100.0f);
        string formattedTime = $"{minutes:0}:{seconds:00}:{mSeconds:00}";
        return formattedTime;
    }
    
    private void SampleTime()
    {
        if (!isTimeMeasured) return;
        // timer = Time.time;
        timer = Time.timeSinceLevelLoad;
    }
}
