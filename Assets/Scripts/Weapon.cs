using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 7 Dec. 2022 by @Nuutti J.
/// </summary>

public class Weapon : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [Tooltip("Fire rates for different weapons (pistol, rifle, shotgun)")]
    [SerializeField] float[] fireRates;

    [Tooltip("Heat increase rates for different weapons (pistol, rifle, shotgun)")]
    [SerializeField] float[] heatIncreaseRates;

    [Tooltip("Rifle projectiles")]
    [SerializeField] int rifleProjectilesAmount = 3;

    [Tooltip("Shotgun projectiles")]
    [SerializeField] int shotgunProjectilesAmount = 5;

    [Tooltip("The spread angle of the projectiles")]
    [Range(0, 45)]
    [SerializeField] float _spreadAngle = 0f;

    [Tooltip("Projectiles max travel distance")]
    [SerializeField] float _range = 0f;

    [Tooltip("Is the weapon burst fire?")]
    [SerializeField] bool _isBurstEnabled = false;

    [Tooltip("The time between each shot in burst")]
    [SerializeField] float _burstFireRate = 0f;

    [Tooltip("Click each shot or hold mouse down?")]
    public bool _allowHold = false;

    [Header("Weapon objects")]
    [Tooltip("What does the weapon shoot (pistol, rifle, shotgun)")]
    [SerializeField] Projectile[] projectiles;

    [Tooltip("The instantiation point of the projectile")]
    [SerializeField] Transform _muzzle;

    // Added by Toni N. - 06122022
    [SerializeField] Slider heatSlider;

    [Tooltip("How much heat does one shot add (out of 100)")]
    [SerializeField] float shotHeatIncrease;

    [Tooltip("How quickly does the heat decrease (out of 100)")]
    [SerializeReference] float shotHeatDecrease;
    // End Added by Toni N.

    /* HIDDEN FIELDS: */
    float nextFire;
    float heatAmount;
    bool isOverheated;

    /* HIDDEN FIELDS: */
    Transform _weaponPivot;

    // Start is called before the first frame update
    void Start() {
        _weaponPivot = GetComponentInParent<Transform>();
    }

    // Added by Toni N. - 06122022
    private void Update() {
        heatSlider.value = heatAmount;
        heatAmount -= shotHeatDecrease * Time.deltaTime;

        // Added by Nuutti J. 07122022
        if (heatAmount > 99f) {
            StartCoroutine(onCooldown());
        }

        if (heatAmount < heatSlider.minValue) {
            heatAmount = heatSlider.minValue;
        }

        if (heatAmount > heatSlider.maxValue) 
        {
            heatAmount = heatSlider.maxValue;
        }
    }

    /* FUNCTIONS */
    public void Shoot() {
        // Shoot if the weapon isn't overheated 
        if(!isOverheated) {
            // Added by Toni N. - 06122022
            // Shoot if the heatAmount doesn't go over the max amount on the next shot
            if (heatAmount < heatSlider.maxValue - shotHeatIncrease) {
                // Shoot if the time of the latest shot has passed the fire rate
                if (Time.time > nextFire) {
                    if (heatAmount >= 0 && heatAmount < 54) {
                        shotHeatIncrease = heatIncreaseRates[0];
                        heatAmount += heatIncreaseRates[0];
                        singleShot();
                    } else if (heatAmount > 55 && heatAmount < 79) {
                        shotHeatIncrease = heatIncreaseRates[1];
                        heatAmount += heatIncreaseRates[1];
                        StartCoroutine(burstShot());
                    } else if (heatAmount > 80) {
                        shotHeatIncrease = heatIncreaseRates[2];
                        heatAmount += heatIncreaseRates[2];
                        shotgunShot();
                    }
                }
            }
        }
    }

    void singleShot() {
        nextFire = Time.time + fireRates[0];
        Quaternion weaponRotation = _weaponPivot.transform.rotation;
        Projectile bullet = projectiles[0];

        GameObject projectile = Instantiate(bullet.gameObject, _muzzle.position, weaponRotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(_muzzle.right * bullet._speed, ForceMode2D.Impulse);
    }

    IEnumerator burstShot() {
        nextFire = Time.time + fireRates[1] + ((rifleProjectilesAmount - 1) * _burstFireRate);
        Projectile bullet = projectiles[1];

        for (int i = 0; i < rifleProjectilesAmount; i++) {
            Quaternion weaponRotation = _weaponPivot.transform.rotation;
            GameObject projectile = Instantiate(bullet.gameObject, _muzzle.position, weaponRotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(_muzzle.right * bullet._speed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(_burstFireRate);
        }
    }

    void shotgunShot() {
        nextFire = Time.time + fireRates[2];
        Projectile bullet = projectiles[2];
        Quaternion weaponRotation = _weaponPivot.transform.rotation;

        // Foreach projectile calculate a random rotation max being the _spreadAngle and add force based on the projectiles new right direction
        for (int i = 0; i < shotgunProjectilesAmount; i++) {
            Quaternion randomRot = Random.rotation;
            GameObject projectile = Instantiate(bullet.gameObject, _muzzle.position, weaponRotation);
            projectile.transform.rotation = Quaternion.RotateTowards(projectile.transform.rotation, randomRot, _spreadAngle);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(projectile.transform.right * bullet._speed, ForceMode2D.Impulse);
        }
    }

    IEnumerator onCooldown() {
        isOverheated = true;
        while(heatAmount > heatSlider.minValue) {
            yield return null;
        }
        isOverheated = false;
    }
}
