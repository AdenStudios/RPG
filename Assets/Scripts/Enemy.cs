using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth = 100;
    private int damage = 8;

    private enum States { Idle, Wandering, Chasing, Attacking }
    private States curState = States.Idle;

    private CharacterController cController;
    private Animator animator;
    private  Player player;

    private bool isPlayerTargeted = false;
    private bool attackInProgress = false;

    NavMeshAgent agent;

    // Idle Variables
    private float idleTimer = 0f;
    private float idleDelay = 0f;
    private float minIdleTime = 20f;
    private float maxIdletime = 40f;

    // Attack Variables
    private float attackDelay = 1.5f;
    private float delayTimer = 1.5f;

    private Vector3 startPosition;
    private Vector3 wanderPosition;
    
    private bool idleDelaySet = false;
    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        startPosition = transform.position;
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        StateManager();
    }

    private void FixedUpdate() 
    {
        if (!isPlayerTargeted) DetectPlayer();
    }

    public void Damage(int amount, Player player)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            player.RemoveTarget(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void StateManager()
    {
        switch (curState)
        {
            case States.Idle:
            if (!idleDelaySet)
            {
                idleDelay = Random.Range(minIdleTime, maxIdletime);
                idleDelaySet = true;
            }
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDelay)
            {
                idleTimer = 0;
                idleDelaySet = false;
                wanderPosition = RandomWanderPoint();
                transform.LookAt(wanderPosition);
                curState = States.Wandering;
            }
            break;

            case States.Wandering:
                if (Vector3.Distance(transform.position, wanderPosition) >= 0.2f)
                {
                    animator.SetBool("Running", true);
                    cController.Move(transform.forward * 2 * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("Running", false);
                    curState = States.Idle;
                }
            break;

            case States.Chasing:
            Vector3 playerPos = player.gameObject.transform.position;
            transform.LookAt(new Vector3(playerPos.x, 0, playerPos.z));
            cController.Move(transform.forward * 4f * Time.deltaTime);
            animator.SetBool("Running", true);
            if (Vector3.Distance(transform.position, player.gameObject.transform.position) <= 2f)
            {
                curState = States.Attacking;
                animator.SetBool("Running", false);
                animator.SetBool("Sheathed", false);
            }
            break;

            case States.Attacking:

                if (!attackInProgress)
                {
                    delayTimer += Time.deltaTime;
                    if (Vector3.Distance(transform.position, player.gameObject.transform.position) > 3f)
                    {
                        curState = States.Chasing;
                        animator.SetBool("Running", true);
                    }
                }

                if (delayTimer >= attackDelay)
                {
                    attackInProgress = true;
                    delayTimer = 0;
                    playerPos = player.gameObject.transform.position;
                    transform.LookAt(new Vector3(playerPos.x, 0, playerPos.z));
                    animator.SetTrigger("Slash");
                    Invoke("FinishAttack", 2f);
                }
            break;           
        }
    }
    private Vector3 RandomWanderPoint()
    {
        Vector3 randomPos = startPosition + Random.insideUnitSphere *  7;
        randomPos.y = 0;
        return randomPos;
    }
    private void DetectPlayer()
    {
        Collider [] hits = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "Player")
            {
                isPlayerTargeted = true;
                curState = States.Chasing;
            }
        }
    }
    private void FinishAttack()
    {
        attackInProgress = false;
    }
}
