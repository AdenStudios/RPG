using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    public bool isSheathed = true;
    private bool attackInProgress = false;
    public  bool playerAttacking {get { return attackInProgress; } }

    private float comboTimer = 0f;
    private float comboMaxDelay = 5f;

    private string [] basicAttacks = new string[2] { "Slash", "SlashCombo" };
    public int basicAttackID = 0;
    

    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponentInChildren<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        comboTimer += Time.deltaTime;
        if (comboTimer > comboMaxDelay)
        {
            basicAttackID = 0;
            comboTimer = 0;
        }
        if (Input.GetMouseButtonDown(0) && !isSheathed && !attackInProgress)
        {                
            //BasicAttack();                 
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isSheathed)
            {
                anim.SetTrigger("Draw");
            }
            else
            {
                anim.SetTrigger("Sheath");
            }
            isSheathed = !isSheathed;
            anim.SetBool("Sheathed", isSheathed);

        }
    }

    public void BasicAttack()
    {
        if (!isSheathed && !attackInProgress)
        {
            attackInProgress = true;
            anim.SetTrigger(basicAttacks[basicAttackID]);

            Invoke("FinishAttackAnimation", 0.7f);    
            if (basicAttackID == 0)
            {
                basicAttackID = 1;
                Invoke("SendDamage", 0.8f);
            }
            else 
            {
                basicAttackID = 0;
                Invoke("SendDamage", 1.2f);
            }   
        } 
        if (isSheathed)
        {
          anim.SetTrigger("Draw");
          isSheathed = false;  
          anim.SetBool("Sheathed", false);
        }
    }

    public void EnableBlock()
    {
        anim.SetBool("Block", true);
    }

    public void DisableBlock()
    {
        anim.SetBool("Block", false);
    }

    private void SendDamage()
    {
        List<GameObject> currentTargets = new List<GameObject>(GetComponent<Player>().playerTargets);

        foreach (var target in currentTargets)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(50, GetComponent<Player>());
            }
        }
    }
    // called from the animator
    private void FinishAttackAnimation()
    {
        attackInProgress = false;
    }
}
