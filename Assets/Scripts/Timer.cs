using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
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
    }

    public static void TurnOffTimer()
    {
        isTimeMeasured = false;
    }

    public static float GetTimer()
    {
        Debug.Log("Timer -> " + timer);
        return timer;
    }
    
    private void SampleTime()
    {
        if (!isTimeMeasured) return;
        // timer = Time.time;
        timer = Time.timeSinceLevelLoad;
        Debug.Log(timer);
    }
    
    
}
