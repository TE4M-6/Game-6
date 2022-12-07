using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 1 Dec. 2022 by @Nuutti J.
/// </summary>

public class Weapon : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [Header("Weapon properties")]

    [Tooltip("How long does it take to shoot another projectile")]
    [SerializeField] float _rateOfFire = 0.33f;

    [Header("Weapon objects")]
    [Tooltip("What does the weapon shoot")]
    [SerializeField] Projectile _projectile;
    
    [Tooltip("The instantiation point of the projectile")]
    [SerializeField] Transform _muzzle;

    [SerializeField] Slider heatSlider;
    [Tooltip("How much heat does one shot add (out of 100)")]
    [SerializeField] float shotHeatValue;

    /* HIDDEN FIELDS: */
    float nextFire;
    float heatAmount;

    /* HIDDEN FIELDS: */
    Transform _weaponPivot;

    // Start is called before the first frame update
    void Start() {
        _weaponPivot = GetComponentInParent<Transform>();
    }

    private void Update()
    {
        heatSlider.value = heatAmount;

        heatAmount -= 5 * Time.deltaTime;

        if (heatAmount > heatSlider.maxValue) 
        {
            heatAmount = heatSlider.maxValue;
        }
    }

    /* FUNCTIONS */
    public void Shoot() {
        // Shoot if the time of the latest shot has passed the fire rate
        if(Time.time > nextFire) {
            if (heatAmount < heatSlider.maxValue - shotHeatValue)
            {
                nextFire = Time.time + _rateOfFire;

                Quaternion rotation = _weaponPivot.transform.rotation;
                GameObject projectile = Instantiate(_projectile.gameObject, _muzzle.position, rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                rb.AddForce(_muzzle.right * _projectile._speed, ForceMode2D.Impulse);
                heatAmount += shotHeatValue;
            }

        }
        
    }

}
