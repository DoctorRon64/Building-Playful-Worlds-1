using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateMachine Controller;
    public PlayerMovement Player;
    public float stoppingDistance;
    public float viewDistance;
    protected Animator anim;
    protected bool AttackingPlayer;
    protected float Health;
    

    public void Initalize(StateMachine owner)
    {
        Controller = owner;
    }

    public virtual void OnEnter()
    {
        anim = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerMovement>();
        Health = 25;
        stoppingDistance = 3;
        viewDistance = 10;
    }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
}
