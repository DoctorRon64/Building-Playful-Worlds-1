using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum StateEnum { Patrol, Detect, Attack }
    public StateEnum currentState;
    public float ViewDistance = 5;

	[Header("NavMeshAgent")]
    public NavMeshAgent NavMeshAgent;
    private PatrolPoints[] patrolPoints;
    private DetectPoints[] detectPoints;
    public int patrolIndex = 0;
    public int detectIndex = 0;

	[Header("Reference Object")]
	public GameObject Player;
	public GameObject DetectPointsLibrary;

	private void Awake()
	{
        NavMeshAgent = GetComponent<NavMeshAgent>();
		patrolPoints = FindObjectsOfType<PatrolPoints>();
		detectPoints = FindObjectsOfType<DetectPoints>();
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
            case StateEnum.Patrol: PatrolBehaviour(); break;
            case StateEnum.Detect: DetectBehaviour(); break;
			case StateEnum.Attack: AttackBehaviour(); break;
   		}
	}
	private void PatrolBehaviour()
	{
        if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
		{
			patrolIndex++;
			if (patrolIndex < patrolPoints.Length)
			{
				NavMeshAgent.SetDestination(patrolPoints[patrolIndex].transform.position);
			}

			if (patrolIndex >= patrolPoints.Length)
			{
				patrolIndex = 0;
			}
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < ViewDistance*2)
		{
			DetectPointsLibrary.transform.position = NavMeshAgent.transform.position;
			currentState = StateEnum.Detect;
			detectIndex = 0;
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < ViewDistance)
		{
			currentState = StateEnum.Detect;
		}
	}

	private void DetectBehaviour()
	{
        if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
		{
			detectIndex++;
			if (detectIndex < detectPoints.Length)
			{
				NavMeshAgent.SetDestination(detectPoints[detectIndex].transform.position);
			}

			if (detectIndex >= detectPoints.Length)
			{
				detectIndex = 0;
			}
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < ViewDistance)
		{
			currentState = StateEnum.Attack;
		}

		if (Vector3.Distance(transform.position, Player.transform.position) > ViewDistance/2)
		{
			patrolIndex = 0;
			currentState = StateEnum.Patrol;
		}
	}

	private void AttackBehaviour()
	{
		NavMeshAgent.SetDestination(Player.transform.position);

		if (Vector3.Distance(transform.position, Player.transform.position) > ViewDistance)
		{
			currentState = StateEnum.Patrol;
		}
	}
}
