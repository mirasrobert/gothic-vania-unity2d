using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    const string PLAYER_ATTACK_ANIM = "Player_Attack_1";

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
 