using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum StateEnum { Idle, Patrol, Attack }
    public StateEnum currentState;
    public float viewDistance = 5;

    public NavMeshAgent navMeshAgent;
    private PatrolPoints[] patrolPoints;
    public int patrolIndex = 0;
    public GameObject Player;

	private void Awake()
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
		patrolPoints = FindObjectsOfType<PatrolPoints>();
	}

	private void Start()
	{
        if (patrolPoints.Length != 0)
		{
            navMeshAgent.SetDestination(patrolPoints[patrolIndex].transform.position);
		}
	}

	private void Update()
	{
        CheckState();
	}

	private void CheckState()
	{
        switch (currentState)
		{
			case StateEnum.Idle: IdleBehaviour(); break;
            case StateEnum.Patrol: PatrolBehaviour(); break;
			case StateEnum.Attack: AttackBehaviour(); break;
   		}
	}

	private void IdleBehaviour()
	{
		currentState = StateEnum.Patrol;
	}

	private void PatrolBehaviour()
	{
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
		{
			patrolIndex++;
			if (patrolIndex < patrolPoints.Length)
			{
				navMeshAgent.SetDestination(patrolPoints[patrolIndex].transform.position);
			}

			if (patrolIndex >= patrolPoints.Length)
			{
				patrolIndex = -1;
			}
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < viewDistance)
		{
			currentState = StateEnum.Attack;
		}
	}

	private void AttackBehaviour()
	{
		navMeshAgent.SetDestination(Player.transform.position);

		if (Vector3.Distance(transform.position, Player.transform.position) > viewDistance)
		{
			//navMeshAgent.speed += 1;
			//currentState = StateEnum.Patrol;
		}
	}
}
