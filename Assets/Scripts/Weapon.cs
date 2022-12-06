using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 7 Dec. 2022 by @Nuutti J.
/// </summary>

public class Weapon : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [Tooltip("Time between shots")]
    [SerializeField] float _rateOfFire = 0.33f;

    [Tooltip("Projectiles per shot")]
    [SerializeField] int _projectilesPerShot = 1;

    [Tooltip("The spread angle of the projectiles")]
    [Range(0, 45)]
    [SerializeField] float _spreadAngle = 0f;

    [Tooltip("Projectiles travel distance")]
    [SerializeField] float _range = 0f;

    [Tooltip("Is the weapon burst fire?")]
    [SerializeField] bool _isBurstEnabled = false;

    [Tooltip("The time between each shot in burst")]
    [SerializeField] float _burstFireRate = 0f;

    [Tooltip("Click each shot or hold mouse down?")]
    public bool _allowHold = false;

    [Header("Weapon objects")]
    [Tooltip("What does the weapon shoot")]
    [SerializeField] Projectile _projectile;

    [Tooltip("The instantiation point of the projectile")]
    [SerializeField] Transform _muzzle;

    /* HIDDEN FIELDS: */
    float nextFire;

    /* HIDDEN FIELDS: */
    Transform _weaponPivot;

    // Start is called before the first frame update
    void Start() {
        _weaponPivot = GetComponentInParent<Transform>();
    }

    /* FUNCTIONS */
    public void Shoot() {
        // Shoot if the time of the latest shot has passed the fire rate
        if (Time.time > nextFire) {

            if (_isBurstEnabled) {
                // Basic rate of fire + how long does it take to finish the burst (-1 because the first shot is instant)
                nextFire = Time.time + _rateOfFire + ((_projectilesPerShot - 1) * _burstFireRate);
            } else {
                nextFire = Time.time + _rateOfFire;
            }

            // Single shot behaviour
            if (_projectilesPerShot == 1) {
                singleShot();
            }

            // Shotgun behaviour
            if (_projectilesPerShot > 1 && _spreadAngle > 0) {
                shotgunShot();
            }

            // Burst behaviour
            if (_projectilesPerShot > 1 && _spreadAngle == 0 && _isBurstEnabled && _burstFireRate > 0) {
                StartCoroutine(burstShot());
            }
        }

    }

    void singleShot() {
        Quaternion weaponRotation = _weaponPivot.transform.rotation;

        GameObject projectile = Instantiate(_projectile.gameObject, _muzzle.position, weaponRotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(_muzzle.right * _projectile._speed, ForceMode2D.Impulse);
    }

    void shotgunShot() {
        Quaternion weaponRotation = _weaponPivot.transform.rotation;

        // Foreach projectile calculate a random rotation max being the _spreadAngle and add force based on the projectiles new right direction
        for (int i = 0; i < _projectilesPerShot; i++) {
            Quaternion randomRot = Random.rotation;
            GameObject projectile = Instantiate(_projectile.gameObject, _muzzle.position, weaponRotation);
            projectile.transform.rotation = Quaternion.RotateTowards(projectile.transform.rotation, randomRot, _spreadAngle);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(projectile.transform.right * _projectile._speed, ForceMode2D.Impulse);
        }
    }

    IEnumerator burstShot() {
        for (int i = 0; i < _projectilesPerShot; i++) {
            Quaternion weaponRotation = _weaponPivot.transform.rotation;
            GameObject projectile = Instantiate(_projectile.gameObject, _muzzle.position, weaponRotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(_muzzle.right * _projectile._speed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(_burstFireRate);
        }
    }
}
