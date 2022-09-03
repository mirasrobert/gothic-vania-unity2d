using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    const string LEFT = "left";
    const string RIGHT = "right";
    string facingDirection;

    [SerializeField] Transform castPos;
    [SerializeField] float baseCaseDistance;

    private Vector3 baseScale;

    [SerializeField] Transform player;

    [SerializeField] float agroRange = 0f;

    [SerializeField] float moveSpeed = 3f;

    Rigidbody2D rb;

    Animator animator;

    // LEFT = FALSE
    // RIGHT = TRUE
    [SerializeField] bool isFlipped = false;

    public float attackRange = 3f;

    public Transform attackPoint;

    public int attackDamage = 1;

    public LayerMask playerLayers;

    // 2f = 2 attack per second
    public float attackRate = 2f;
    float nextAttackTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);

        float moveX = moveSpeed;

        if (facingDirection == LEFT)
        {
            moveX = -moveSpeed;
        }

        // If Player is on ENEMY AGRO RANGE
        if ((distanceToPlayer < agroRange))
        {
            // Chase Player
            ChasePlayer(newPos);
        }
        else
        {
            // Stop Chasing the player and do something
            animator.SetBool("IsRunning", true);

            // Movement speed of the gameobject
            rb.velocity = new Vector2(moveX, rb.velocity.y);

            // Change Direction if near edge or near a wall
            if (isHittingWall() || isNearEdge())
            {
                if (facingDirection == LEFT)
                {
                    ChangeFaceDirection(RIGHT);
                    isFlipped = true;
                }
                else if (facingDirection == RIGHT)
                {
                    ChangeFaceDirection(LEFT);
                    isFlipped = false;
                }
            }
        }


    }

    void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
            facingDirection = LEFT;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
            facingDirection = RIGHT;
        }
    }

    void ChasePlayer(Vector2 newPos)
    {
        LookAtPlayer();
        animator.SetBool("IsRunning", true);

        // Chase the player
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            // Prevent for spamming attack
            if (Time.time >= nextAttackTime)
            {
                PlayAttackAnimation();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }
        else
        {
            animator.ResetTrigger("Attack");
        }
    }

    void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    // This will be used on Animation Event
    void AttackPlayer()
    {
        // Detect enemy in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        // Damage enemy
        foreach (Collider2D player in hitEnemies)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void ChangeFaceDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x; 
        }
        else
        {   // Right
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;
        facingDirection = newDirection;
    }
    bool isHittingWall()
    {
        bool val = false;
        float castDist = baseCaseDistance;

        // Define the cast distance for left and right
        if (facingDirection == LEFT)
        {
            castDist = -baseCaseDistance;
        }
        else
        {
            castDist = baseCaseDistance;
        }

        // Determine the target destination based on cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        // Draw a Line
        Debug.DrawLine(castPos.position, targetPos, Color.blue);


        // Check if cast hit a foreground
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Foreground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    bool isNearEdge()
    {
        bool val = true;
        float castDist = baseCaseDistance;

        // Determine the target destination based on cast distance
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        // Draw a Line
        Debug.DrawLine(castPos.position, targetPos, Color.green);


        // Check if cast does not hit cast hit a foreground
        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Foreground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
