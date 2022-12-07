using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: @Daniel K.
/// Last modified: 05 Dec 2022 by @Toni N.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    /* EXPOSED FIELDS: */
    [Header("PLAYER: ")] 
    [SerializeField] private float hitPoints = 100.0f;

    [SerializeField] private Slider healthSlider;

    private void Update()
    {
        healthSlider.value = hitPoints;
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0.0f)
        {
            hitPoints = 0.0f;
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<EndOfGameScript>().GameOver();
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAiming>().enabled = false;
    }
}
