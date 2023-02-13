using UnityEngine;

public class AttackState : State
{   
    public override void OnEnter()
    {
        base.OnEnter();
        anim.SetInteger("EnemyState", 2);
        AttackingPlayer = true;
    }

    public override void OnUpdate()
    {
        //Transition
        if(Vector3.Distance(transform.position, Player.transform.position) > viewDistance)
        {
            Controller.SwitchState(typeof(PatrolState));
        }

        //Move
        Controller.Controller.MoveToPosition(Player.transform.position);

        //Rotate
        Controller.Controller.MoveToRotation(Player.transform.position);
    }
}
