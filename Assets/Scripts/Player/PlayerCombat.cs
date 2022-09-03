using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 1;


    // 2f = 2 attack per second
    public float attackRate = 2f;
    float nextAttackTime = 0f;


    // Update is called once per frame
    void Update()
    {
        // Prevent for spamming attack
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayAttackAnimation();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // This will be used on Animation Event
    public void AttackEnemy()
    {
        // Detect enemy in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void PlayAttackAnimation()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");
    }

    private void OnDrawGizmosSelected ()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
 