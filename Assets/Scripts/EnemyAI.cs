using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// AUTHOR: @Joona H.
/// Last modified: 08 Dec 2022 by @Daniel K.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    /* EXPOSED FIELDS: */
    public Transform target;
    public Transform enemyGraphics;
    public float speed = 200f;
    public float waypointDistance = 3f; 
    [SerializeField] [Range(1.0f, 20.0f)] 
    private float chaseRadius = 3.0f;
    
    /* HIDDEN FIELDS: */
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private float distanceToTarget = Mathf.Infinity;
    

    Seeker seeker;
    Rigidbody2D enemyRb;
    private Rigidbody2D playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        enemyRb = GetComponent<Rigidbody2D>();
        playerRb = target.GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(enemyRb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SampleTargetDistance();
        Move();
    }

    private void SampleTargetDistance()
    {
        distanceToTarget = Vector2.Distance(target.position, transform.position);
    }
    
    private void Move()
    {
        // Conditions:
        // if (CameraController.GetIsInRoom()) return;
        if (!(distanceToTarget < chaseRadius)) return;
        
        // Code:
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 direction = (playerRb.position - enemyRb.position).normalized; // @Dan - 02.12.2022
        Vector2 force = direction * (speed * Time.deltaTime);

        enemyRb.AddForce(force);

        float distance = Vector2.Distance(enemyRb.position, path.vectorPath[currentWaypoint]);

        if (distance < waypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGraphics.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            ;
        }
        else if (force.x <= -0.01f)
        {
            enemyGraphics.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
}
