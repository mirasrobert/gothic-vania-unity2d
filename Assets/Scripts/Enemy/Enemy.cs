using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play Hurt Animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            rb.velocity = new Vector2(0, 0); // Stop Enemy from moving when dies
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
