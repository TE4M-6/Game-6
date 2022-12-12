using UnityEngine;

/// <summary>
/// AUTHOR: @Toni
/// Last modified: 12 Dec. 2022 by @Daniel K.
/// </summary>
///
/// 
public class LevelStartScript : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject hud;

    private GameObject [] cutScenes;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter.SetActive(false);
        hud.SetActive(false);
        playerWeapon.SetActive(false);
        playerCharacter.GetComponent<PlayerAiming>().enabled = false;
        playerCharacter.GetComponent<PlayerShooting>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayCutscenes();
    }

    private void PlayCutscenes()
    {
        cutScenes = GameObject.FindGameObjectsWithTag("Cutscene Canvas");
        if (cutScenes.Length == 0)
        {
            playerCharacter.SetActive(true);
            hud.SetActive(true);
        }
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
