using UnityEngine;
/// <summary>
/// AUTHOR: @Daniel K.
/// Last modified: 13 Dec. 2022 by @Daniel K.
/// </summary>
/// 
public class LevelExitDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Timer.GetTimer();
            FindObjectOfType<EndOfGameScript>().GameWon();
        }
    }
}
