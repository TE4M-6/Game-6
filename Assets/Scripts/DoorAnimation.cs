using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AUTHOR: @Joona H.
/// Last modified: 8 Dec 2022 by @Joona H.
/// </summary>
public class DoorAnimation : MonoBehaviour
{
    /*HIDDEN FIELDS*/
    private Animator _animator = null;
    [SerializeField] AudioClip doorOpen;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "PlayerFeet")
        {
            SoundManager.instance.PlaySingle(doorOpen);
            _animator.Play("Door_Open_Animation");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerFeet")
        {
            SoundManager.instance.PlaySingle(doorOpen);
            _animator.Play("Door_Close_Animation");
        }
    }
}
