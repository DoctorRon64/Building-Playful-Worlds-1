using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum StateEnum { Patrol, Detect, Attack }
    public StateEnum currentState;
    public float viewDistance = 5;

    public NavMeshAgent navMeshAgent;
    private PatrolPoints[] patrolPoints;
    private DetectPoints[] detectPoints;
    public int patrolIndex = 0;
    public int detectIndex = 0;

    public GameObject Player;
	public GameObject DetectPointsLibrary;

	private void Awake()
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
		patrolPoints = FindObjectsOfType<PatrolPoints>();
		detectPoints = FindObjectsOfType<DetectPoints>();
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
            case StateEnum.Patrol: PatrolBehaviour(); break;
            case StateEnum.Detect: DetectBehaviour(); break;
			case StateEnum.Attack: AttackBehaviour(); break;
   		}
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
				patrolIndex = 0;
			}
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < viewDistance*2)
		{
            currentState = StateEnum.Detect;
			detectIndex = 0;
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < viewDistance)
		{
			currentState = StateEnum.Detect;
		}
	}

	private void DetectBehaviour()
	{
        DetectPointsLibrary.transform.position = navMeshAgent.transform.position;

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
		{
			detectIndex++;
			if (detectIndex < detectPoints.Length)
			{
				navMeshAgent.SetDestination(detectPoints[detectIndex].transform.position);
			}

			if (detectIndex >= detectPoints.Length)
			{
				detectIndex = 0;
			}
		}

		if (Vector3.Distance(transform.position, Player.transform.position) < viewDistance)
		{
			currentState = StateEnum.Attack;
		}

		if (Vector3.Distance(transform.position, Player.transform.position) > viewDistance/2)
		{
			patrolIndex = 0;
			currentState = StateEnum.Patrol;
		}
	}

	private void AttackBehaviour()
	{
		navMeshAgent.SetDestination(Player.transform.position);

		if (Vector3.Distance(transform.position, Player.transform.position) > viewDistance)
		{
			currentState = StateEnum.Patrol;
		}
	}
}
