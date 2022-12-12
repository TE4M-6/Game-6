using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 12 Dec 2022 by @Toni N.
/// </summary>

public class Projectile : MonoBehaviour {

    [SerializeField] private GameObject bloodSplatter;
    [SerializeField] private GameObject projectileDestroyed;

    /* EXPOSED FIELDS: */
    public float _speed = 10f;
    public float _damage = 1f;
    private void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.CompareTag("Player")) {
            Instantiate(projectileDestroyed, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Added by Joona H. - 01122022
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(_damage);
            Instantiate(bloodSplatter, collision.gameObject.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.GetComponent<ObstacleHealth>().TakeDamage(_damage);
        }

    }

}
