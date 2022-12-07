using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 7 Dec 2022 by @Nuutti J.
/// </summary>

public class PlayerAiming : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [Header("Rotation")]
    [Tooltip("The point where the weapon rotation pivots around")]
    [SerializeField] GameObject pivot;

    [Tooltip("A zone to restrict the rotation in")]
    [SerializeField] Collider2D exclusionZone;

    [Header("Weapon hiding (sorting layers)")]
    [Tooltip("The object to hide behind")]
    [SerializeField] SpriteRenderer playerRenderer;

    [Tooltip("The angle to start hiding")]
    [SerializeField] float hideThresholdStart = 75;

    [Tooltip("The angle to stop hiding")]
    [SerializeField] float hideThresholdEnd = 125;

    /* HIDDEN FIELDS: */
    Vector3 mouseWorldPos;
    Vector2 rotateDir;
    SpriteRenderer weaponRenderer;

    void Awake() {
        weaponRenderer = GetComponentInChildren<Weapon>().GetComponent<SpriteRenderer>();    
    }

    void Update() {
        // The position of the cursor in unity coordinates
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Just for stroring the scale
        Vector2 weaponScale = pivot.transform.localScale;

        // Only rotate if not in exclusion zone
        // (quick fix for the wonky behaviour when the cursor is near the pivot point)
        if (!isInExclusionZone()) {
            // Direction between the cursor position and the weapons pivot point
            rotateDir = (mouseWorldPos - pivot.transform.position).normalized;

            // Change the weapon pivots right direction (red axis, x) to face the rotation direction
            pivot.transform.right = rotateDir;

            // Hide the weapon if it is rotated "above" the players head
            if (pivot.transform.eulerAngles.z > hideThresholdStart && pivot.transform.eulerAngles.z < hideThresholdEnd) {
                weaponRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
            } else {
                weaponRenderer.sortingOrder = playerRenderer.sortingOrder + 1;
            }
        }

        // Flip the gun depending on the side the cursor is on
        // If flipped inside the exclusion zone, just invert the latest stored rotateDir x-axis
        if (mouseWorldPos.x < transform.position.x) {
            if(isInExclusionZone()) {
                pivot.transform.right = new Vector2(-rotateDir.x, rotateDir.y);
            }
            
            weaponScale.x = -1;
            weaponScale.y = -1;
            pivot.transform.localScale = weaponScale;
        } else if (mouseWorldPos.x > transform.position.x) {
            if (isInExclusionZone()) {
                pivot.transform.right = rotateDir;
            }

            weaponScale.x = 1;
            weaponScale.y = 1;
            pivot.transform.localScale = weaponScale;
        }
    }

    /* FUNCTIONS */
    bool isInExclusionZone() {
        return exclusionZone.OverlapPoint(mouseWorldPos);
    }
}
