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
        if (!walkingPointSet)
        {
            SearchWalkPoint();
        }

        if (walkingPointSet)
        {
            agent.SetDestination(walkingPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkingPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkingPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkingPointRange, walkingPointRange);
        float randomX = Random.Range(-walkingPointRange, walkingPointRange);

        walkingPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkingPoint, -transform.up, 2f, whatIsGround))
        {
            walkingPointSet = true;
        }
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


}
