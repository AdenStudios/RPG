using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfEnemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent navMesh;

    

    public GameObject wolf;
    public GameObject player;
    public Transform target;

    private Vector3 destination;

    public float wolfRadius = 10;

    private BoxCollider boxColl;

    private bool canChase = false;
    

    private Animator anim;
    private bool chasing = false;
    private bool canAttack = false;
    private float currentHealth = 100;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (navMesh.stoppingDistance > 2)
        {
            Chase();
        }
        else if(navMesh.stoppingDistance ==2)
        {
            Attack();
        }
       

        
              
    }

    void Attack()
    {
        
        
        anim.SetBool("Run", false);
        anim.SetBool("Claw Attack", true);

        
    }
    void Roam()
    {
        anim.SetBool("Claw Attack", false);
        anim.SetBool("Run", false);
    }
    void Chase()
    {
        
        anim.SetBool("Run", true);    
        transform.LookAt(target.transform.position);
        float dist = Vector3.Distance(target.transform.position, this.transform.position);
        if (dist < wolfRadius)
        {
            destination = target.position;
            navMesh.destination = destination;

        }
        


    }
    
    
    void OnTriggerEnter(Collider col)
    {         
        canChase = true;      
    }
    private void OnTriggerExit(Collider other)
    {
        canChase = false;
    }

    public void Damage(int amount, Player player)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            player.RemoveTarget(this.gameObject);
            anim.SetTrigger("Die");
            Destroy(this.gameObject, 1.5f);
        }
    }
}
