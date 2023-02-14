using UnityEngine;

public class IdleState : State
{
    [SerializeField] private float maxTime = 3f;
    [SerializeField] private float minTime = 1f;
    private float waitTime = 0;
    public override void OnEnter()
    {
        base.OnEnter();
        anim.SetInteger("EnemyState", 0);
        AttackingPlayer = false;

        waitTime = Random.Range(minTime, maxTime);
    }

    public override void OnUpdate()
    {
        waitTime -= Time.deltaTime;
        if(waitTime <= 0)
        {
            Debug.Log(typeof(PatrolState).Name);
            Controller.SwitchState(typeof(PatrolState));
        }

        if (Vector3.Distance(transform.position, Player.transform.position) < viewDistance)
        {
            Controller.SwitchState(typeof(AttackState));
        }
    }
}