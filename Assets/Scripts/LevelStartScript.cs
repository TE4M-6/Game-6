using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScript : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private GameObject playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon.SetActive(false);
        playerCharacter.GetComponent<PlayerAiming>().enabled = false;
        playerCharacter.GetComponent<PlayerShooting>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerFeet"))
        {
            playerWeapon.SetActive(true);
            playerCharacter.GetComponent<PlayerAiming>().enabled = true;
            playerCharacter.GetComponent<PlayerShooting>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
