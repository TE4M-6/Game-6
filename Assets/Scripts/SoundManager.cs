using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// AUTHOR: @Joona H.
/// Last modified: 7 Dec 2022 by @Joona H.
/// </summary>
public class SoundManager : MonoBehaviour
{
    /* EXPOSED FIELDS: */
    public AudioSource sndEffect;
    public AudioSource music;

    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    /* Play a sound effect one time */
    public void PlaySingle(AudioClip clip)
    {
        sndEffect.clip = clip;
        sndEffect.PlayOneShot(clip);
    }
    /* Play music */
    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

}
