using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play Hurt Animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Die Animation
        animator.SetBool("IsDead", true);
    }

    // This will be called on animation event after DIE animation is done
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

}
