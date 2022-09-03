using System.Collections;
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
    [SerializeField] PlayerMovement playerMovementScript;

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

        healthText.text = Mathf.Clamp(currentHealth, 0, 100).ToString();

        // Play Hurt Animation
        animator.SetTrigger("Hurt");

        Debug.Log("Player is Hurt");

        if (currentHealth <= 0)
        {
            playerMovementScript.enabled = false;
            rb.velocity = new Vector2(0, 0);
            Die();
        } 

    }

    void Die()
    {
        // Die Animation
        animator.SetBool("IsDead", true);
        Debug.Log("Player is Dead");
    }

    // This will be called on animation event after DIE animation is done
    public void DestroyObject()
    {
        //this.enabled = false;
        Destroy(this.gameObject);
    }
}
