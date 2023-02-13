using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public float moveSpeed = 3f;
    [SerializeField] private NavMeshAgent agent;
    public float Health;
    public PlayerMovement PlayerScript;

    private StateMachine stateMachine;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        PlayerScript = FindObjectOfType<PlayerMovement>();

        
    }
    private void Start()
    {
        stateMachine = new StateMachine(this, GetComponents<State>());
        stateMachine.SwitchState(typeof(PatrolState));
    }

    public void Update()
    {
        stateMachine?.OnUpdate();
        IfHealthIsToLow();
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void MoveToRotation(Vector3 targetPosition)
    {
        agent.transform.rotation = Quaternion.LookRotation((targetPosition - transform.position).normalized);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Sword"))
        {
            if (PlayerScript.AttackingEnemy == true)
            {
                Health -= PlayerScript.AttackDamage;
            }
        }
    }
    private void IfHealthIsToLow()
    {
        if (Health < 0)
        {
            Destroy(gameObject);
        }
    }
}
