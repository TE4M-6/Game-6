using System.Collections;
using UnityEngine;

/// <summary>
/// AUTHOR: @Daniel K.
/// Last modified: 12 Dec 2022 by @Toni N.
/// </summary>
/// 
public class ObstacleHealth : MonoBehaviour
{
    /* EXPOSED FIELDS */
    [SerializeField] float hitPoint = 100f;
    [SerializeField] private DamageScript flash;
    [SerializeField] AudioClip explosionSound;

    /* HIDDEN FIELDS */
    private Animator _animator = null;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        hitPoint -= damage;
        flash.Flash();
        if (hitPoint <= 0)
        {
            hitPoint = 0;
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {
        _animator.Play("Explosion");
        SoundManager.instance.PlaySingle(explosionSound);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
