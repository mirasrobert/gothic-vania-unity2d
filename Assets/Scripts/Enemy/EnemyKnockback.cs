using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().KBCounter = collision.gameObject.GetComponent<PlayerMovement>().KBTotalTime;

            if(collision.transform.position.x <= transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerMovement>().KnockFromRight = true;
            }

            if (collision.transform.position.x > transform.position.x)
            {
                collision.gameObject.GetComponent<PlayerMovement>().KnockFromRight = false;
            }

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
