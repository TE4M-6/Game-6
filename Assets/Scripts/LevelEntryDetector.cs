using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntryDetector : MonoBehaviour
{
    [SerializeField] AudioClip levelMusic;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlayMusic(levelMusic);
            gameObject.SetActive(false);
        }
    }
}
