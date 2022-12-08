using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 8 Dec. 2022 by @Nuutti J.
/// </summary>

public class Weapon : MonoBehaviour {
    /* EXPOSED FIELDS: */
    [Header("WEAPON PROPERTIES")]

    [Tooltip("Fire rates for different weapons (pistol, rifle, shotgun)")]
    [SerializeField] float[] fireRates;

    [Tooltip("Heat increase rates for different weapons (pistol, rifle, shotgun)")]
    [SerializeField] float[] heatIncreaseRates;

    [Tooltip("Thresholds for when to change weapon behavior (pistol, rifle). Add .99 to values end (i.e. 49.99, 79.99). Other thresholds will be calculated based on these. Pistol = 0 - 49.99, Rifle = 50 - 79.99, Shotgun = 80 - 100")]
    [SerializeField] float[] heatThresholds;

    [Tooltip("Rifle projectiles")]
    [SerializeField] int rifleProjectilesAmount = 3;

    [Tooltip("Shotgun projectiles")]
    [SerializeField] int shotgunProjectilesAmount = 5;

    [Tooltip("The spread angle of the projectiles")]
    [Range(0, 45)]
    [SerializeField] float _spreadAngle = 0f;

    [Tooltip("The time between each shot in burst")]
    [SerializeField] float _burstFireRate = 0f;

    // Added by Toni N. - 06122022
    [Tooltip("How quickly does the heat decrease (out of 100)")]
    [SerializeReference] float shotHeatDecrease;

    [Tooltip("Click each shot or hold mouse down?")]
    public bool _allowHold = false;

    [Header("WEAPON OBJECTS")]
    [Tooltip("What does the weapon shoot (pistol, rifle, shotgun)")]
    [SerializeField] Projectile[] projectiles;

    [Tooltip("The instantiation point of the projectile")]
    [SerializeField] Transform _muzzle;

    // Added by Toni N. - 06122022
    [SerializeField] Slider heatSlider;

    /* HIDDEN FIELDS: */
    float nextFire;
    float heatAmount;
    float shotHeatIncrease;
    bool isOverheated;

    float pistolThres;
    float rifleThresLower;
    float rifleThresUpper;
    float shotgunThres;

    /* HIDDEN FIELDS: */
    Transform _weaponPivot;

    void Awake() {
        _weaponPivot = GetComponentInParent<Transform>();
        pistolThres = heatThresholds[0];
        rifleThresLower = Mathf.Ceil(pistolThres);
        rifleThresUpper = heatThresholds[1];
        shotgunThres = Mathf.Ceil(rifleThresUpper);
        Debug.Log(shotgunThres);
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

                    // Thersholds for different weapon behaviors
                    if (heatAmount >= 0f && heatAmount < pistolThres) {
                        shotHeatIncrease = heatIncreaseRates[0];
                        heatAmount += heatIncreaseRates[0];
                        singleShot();
                    } else if (heatAmount > rifleThresLower && heatAmount < rifleThresUpper) {
                        shotHeatIncrease = heatIncreaseRates[1];
                        heatAmount += heatIncreaseRates[1];
                        StartCoroutine(burstShot());
                    } else if (heatAmount > shotgunThres) {
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
        float decreaseRate = shotHeatDecrease;

        while(heatAmount > heatSlider.minValue) {

            // shotHeatDecrease is faster on rifle range and even faster on pistol range
            if(heatAmount < rifleThresUpper && heatAmount > rifleThresLower) {
                shotHeatDecrease = decreaseRate * 2;
            } else if (heatAmount < rifleThresLower) {
                shotHeatDecrease = decreaseRate * 3;
            }
            yield return null;
        }

        // Reset values
        shotHeatDecrease = decreaseRate;
        isOverheated = false;
    }
}
