using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /* EXPOSED FIELDS */
    [SerializeField] private GameObject enemy;
    [SerializeField] [Range(0,20)]private int max = 15;
    [SerializeField] [Range(0, 15)] private float spawnCooldown = 3.0f;
    [SerializeField] private GameObject aimingTool;
    [SerializeField] private float forceAmplifier = 2;

    /* HIDDEN FIELDS */
    private bool isTriggered;
    private float time;
    private int counter;

    void Start()
    {
        time = Time.time;
    }
    
    void Update()
    {
        CreateEnemy();
    }

    public void SetTrigger(bool boolean)
    {
        isTriggered = boolean;
    }

    private void CreateEnemy()
    {
        if (!isTriggered) return;
        if (counter == max) return;
        
        if (time + spawnCooldown <= Time.time)
        {
            var spawner = transform;
            Vector2 direction = (aimingTool.transform.position - spawner.transform.position).normalized;
            var obj = Instantiate(enemy, spawner.position, spawner.rotation, spawner);
            obj.SetActive(true);
            obj.GetComponent<Rigidbody2D>().AddForce(forceAmplifier * direction, ForceMode2D.Impulse);

            time = Time.time;
            counter++;
        }
    }
}
