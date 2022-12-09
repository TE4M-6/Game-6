using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Timer.TurnOffTimer();
            Timer.GetTimer();
            FindObjectOfType<EndOfGameScript>().GameWon();
        }
    }
}
