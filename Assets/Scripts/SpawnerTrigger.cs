using System;
using UnityEngine;

/// <summary>
/// AUTHOR: @Daniel K.
/// Last modified: 7 Dec. 2022 by @Daniel K.
/// </summary>
public class SpawnerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Player")) return;
        GetComponentInParent<Spawner>().SetTrigger(true);
    }
}
