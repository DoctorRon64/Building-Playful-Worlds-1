using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateMachine Controller;
    protected Animator anim;
    protected bool AttackingPlayer;
    protected float stoppingDistance;
    protected float Health;
    public PlayerMovement Player;

    public void Initalize(StateMachine owner)
    {
        Controller = owner;
    }

    public virtual void OnEnter()
    {
        anim = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerMovement>();
        Health = 25;
        stoppingDistance = 10;
    }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
}
