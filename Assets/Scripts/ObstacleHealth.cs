using UnityEngine;

/// <summary>
/// AUTHOR: @Daniel K.
/// Last modified: 09 Dec 2022 by @Daniel K.
/// </summary>
/// 
public class ObstacleHealth : MonoBehaviour
{
    /* EXPOSED FIELDS */
    [SerializeField] float hitPoint = 100f;

    public void TakeDamage(float damage)
    {
        hitPoint -= damage;
        if (hitPoint <= 0)
        {
            hitPoint = 0;
            Destroy(gameObject);
        }
    }
}
