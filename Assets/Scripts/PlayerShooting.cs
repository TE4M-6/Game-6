using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 7 Dec. 2022 by @Nuutti J.
/// </summary>

public class PlayerShooting : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [SerializeField] InputActionReference _shootInput;

    /* HIDDEN FIELDS: */
    Weapon _equippedWeapon;
    bool isShooting;

    // Subscribtion of the shoot button to PerformShoot method
    void OnEnable() {
        _shootInput.action.performed += PerformShoot;
        _shootInput.action.canceled += PerformShoot;
    }

    // Unsubscribe
    void OnDisable() {
        _shootInput.action.performed -= PerformShoot;
        _shootInput.action.canceled -= PerformShoot;
    }

    void Start() {
        _equippedWeapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update() {
        // If the shoot button is pressed or held down
        if (isShooting) {
            _equippedWeapon.Shoot();
        }
    }

    /* FUNCTIONS */
    void PerformShoot(InputAction.CallbackContext context) {
        isShooting = false;

        // Check that the player has a weapon child
        if (_equippedWeapon == null) {
            Debug.Log("Player equipped weapon is null ", gameObject);
            return;
        }

        if (_equippedWeapon._allowHold) {
            // Clicked / held
            if (context.performed) {
                isShooting = true;
            }

            // Released
            if (context.canceled) {
                isShooting = false;
            }
        } else {
            if (context.performed) {
                _equippedWeapon.Shoot();
            }
        }
    }
}
