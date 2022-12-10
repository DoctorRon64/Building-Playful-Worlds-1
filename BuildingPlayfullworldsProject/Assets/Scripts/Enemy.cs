using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum StateEnum { Idle, Patrol, Attack }
    public StateEnum currentState;
    public float ViewDistance = 5;
	public float CooldownTimer;
	public float CooldownStartTime;

	[Header("NavMeshAgent")]
    public NavMeshAgent NavMeshAgent;
    private PatrolPoints[] patrolPoints;
    public int patrolIndex = 0;

	[Header("Reference Object")]
	public GameObject Player;

	private void Awake()
	{
        NavMeshAgent = GetComponent<NavMeshAgent>();
		patrolPoints = FindObjectsOfType<PatrolPoints>();
	}

	private void Start()
	{
        if (patrolPoints.Length != 0)
		{
            NavMeshAgent.SetDestination(patrolPoints[patrolIndex].transform.position);
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
		if (Vector3.Distance(transform.position, Player.transform.position) < ViewDistance)
		{
			currentState = StateEnum.Attack;
		}

		CooldownTimer = CooldownStartTime;
		CooldownTimer -= 1000 * Time.deltaTime;
		if (CooldownTimer <= 0)
		{
			NavMeshAgent.SetDestination(patrolPoints[patrolIndex].transform.position);
			currentState = StateEnum.Patrol;
		}

	}

	private void PatrolBehaviour()
	{
        if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
		{
			patrolIndex++;
			if (patrolIndex < patrolPoints.Length)
			{
				currentState = StateEnum.Idle;
			}

			if (patrolIndex >= patrolPoints.Length)
			{
				patrolIndex = -1;
			}
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < ViewDistance)
		{
			currentState = StateEnum.Attack;
		}
	}

	private void AttackBehaviour()
	{
		NavMeshAgent.SetDestination(Player.transform.position);

		if (Vector3.Distance(transform.position, Player.transform.position) > ViewDistance*2)
		{
			currentState = StateEnum.Patrol;
		}
	}
}
