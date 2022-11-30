using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 30 Nov 2022 by @Nuutti J.
/// </summary>

public class PlayerAiming : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [Header("Rotation")]
    [Tooltip("The point where the weapon rotation pivots around")]
    [SerializeField] GameObject weaponPivot;

    [Header("Weapon hiding (sorting layers)")]
    [Tooltip("The object to hide behind")]
    [SerializeField] SpriteRenderer playerRenderer;

    [Tooltip("The weapon to hide")]
    [SerializeField] SpriteRenderer weaponRenderer;

    [Tooltip("The angle to start hiding")]
    [SerializeField] float hideWeaponThresholdMin = 75;

    [Tooltip("The angle to stop hiding")]
    [SerializeField] float hideWeaponThresholdMax = 125;

    void Update() {
        // The position of the cursor in unity coordinates
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Direction between the cursor position and the weapons pivot point
        Vector2 rotateDir = (mouseWorldPos - weaponPivot.transform.position).normalized;

        // Change the weapon pivots right direction (red axis, x) to face the rotation direction
        weaponPivot.transform.right = rotateDir;

        // Just for stroring the scale
        Vector2 weaponScale = weaponPivot.transform.localScale;

        // Hide the weapon if it is rotated "above" the players head
        if (weaponPivot.transform.eulerAngles.z > hideWeaponThresholdMin && weaponPivot.transform.eulerAngles.z < hideWeaponThresholdMax) {
            weaponRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
        } else {
            weaponRenderer.sortingOrder = playerRenderer.sortingOrder + 1;
        }

        // Flip the player and the gun depending on the side the cursor is on
        if (mouseWorldPos.x < transform.position.x) {
            // transform.localScale = new Vector3(-1, 1, 1);
            weaponScale.x = -1;
            weaponScale.y = -1;
            weaponPivot.transform.localScale = weaponScale;
        } else if(mouseWorldPos.x > transform.position.x) {
            // transform.localScale = new Vector3(1, 1, 1);
            weaponScale.x = 1;
            weaponScale.y = 1;
            weaponPivot.transform.localScale = weaponScale;
        }

    }
}
