﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] int maxHealth = 100;
    int currentHealth;
    public Animator animator;


    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString(); 
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthText.text = currentHealth.ToString();

        // Play Hurt Animation
        animator.SetTrigger("Hurt");

        Debug.Log("Player is Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        // Die Animation
        rb.velocity = new Vector2(0, 0);
        animator.SetBool("IsDead", true);
        Debug.Log("Player is Dead");
    }

    // This will be called on animation event after DIE animation is done
    public void DestroyObject()
    {
        this.enabled = false;
        //Destroy(this.gameObject);
    }
}