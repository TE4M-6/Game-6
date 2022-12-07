using System;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Player")) return;
        GetComponentInParent<Spawner>().SetTrigger(true);
    }
}
