using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] [Range(0, 15)] private float _spawnRate = 3.0f;
    [SerializeField] private GameObject aimingTool;

    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_time + _spawnRate <= Time.time)
        {
            Debug.Log("Enemy spawned!");
            var spawner = transform;
            Vector2 direction = new Vector2(50, 50);
            var obj = Instantiate(enemy, spawner.position, spawner.rotation, spawner);
            obj.SetActive(true);
            obj.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            _time = Time.time;
        }
    }
    
    
}
