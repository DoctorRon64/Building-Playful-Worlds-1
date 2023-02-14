using UnityEngine;

public class PatrolState : State
{
    public Transform[] patrolPoints;
    [SerializeField] private int index = -1;
    
    public override void OnEnter()
    {
        base.OnEnter();
        anim.SetInteger("EnemyState", 1);
        AttackingPlayer = false;

        //assign patroplpoints index
        index++;
        if(index >= patrolPoints.Length)
        {
            index = 0;
        }
    }

    public override void OnUpdate()
    {
        //Transition
        if (Vector3.Distance(transform.position, patrolPoints[index].position) < stoppingDistance)
        {
            Controller.SwitchState(typeof(IdleState));
        }

        //Move
        Controller.Controller.MoveToPosition(patrolPoints[index].position);

        if (Vector3.Distance(transform.position, Player.transform.position) < viewDistance)
        {
            Controller.SwitchState(typeof(AttackState));
        }
    }
}
