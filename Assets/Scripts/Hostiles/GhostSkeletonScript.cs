using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostSkeletonScript : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling spots
    public Vector3 walkingPoint;
    bool walkingPointSet;
    public float walkingPointRange;

    //Make patrol points
    [SerializeField] private GameObject[] patrolPoint;
    private int currentWaypointIndex = 0;

    //Attacking state
    public float attackRate;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Combat Engineer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInAttackRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    
    }

    private void Patroling()
    {
        //Checks the current patrol point the hostile entity is on
        if (Vector3.Distance(patrolPoint[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            //Changes the point once it reaches the current patrol point
            currentWaypointIndex++;

            //Resets index to 0 at the end of the array
            if (currentWaypointIndex >= patrolPoint.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        //Makes the hostile entity follow the patrol point
        agent.SetDestination(patrolPoint[currentWaypointIndex].transform.position);
    }

    private void ChasePlayer()
    {
        //Hunts the player down
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Stops the enemy from moving any further
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackRate);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if(health < 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
