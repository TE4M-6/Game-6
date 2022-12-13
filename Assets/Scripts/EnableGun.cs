using UnityEngine;

/// <summary>
/// AUTHOR: @Toni
/// Last modified: 12 Dec. 2022 by @Daniel K.
/// </summary>
///
public class EnableGun : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private GameObject playerCharacter;
    // [SerializeField] private GameObject hud;

    private GameObject [] cutScenes;

    // Start is called before the first frame update
    void Start()
    {
        // hud.SetActive(false);
        playerWeapon.SetActive(false);
        playerCharacter.GetComponent<PlayerAiming>().enabled = false;
        playerCharacter.GetComponent<PlayerShooting>().enabled = false;
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
