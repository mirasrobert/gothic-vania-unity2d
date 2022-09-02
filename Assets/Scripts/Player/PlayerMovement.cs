using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb2d;

    private float horizontalInput = 0f;
    private bool IS_GROUNDED = true;

    [SerializeField] public float speed = 5f;
    [SerializeField] public float jumpForce = 5f;

    public bool inIdleState = true;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;


    const string PLAYER_IDLE_ANIM = "Player_Idle";
    const string PLAYER_RUN_ANIM = "Player_Run";
    const string PLAYER_JUMP_ANIM = "Player_Jump";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if player is on ground or not
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Foreground")) ||
            Physics2D.Linecast(transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer("Foreground")) ||
            Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Foreground")))
        {
            IS_GROUNDED = true;

            // If on ground
            animator.SetBool("IsJumping", false);
        }
        else
        {
            IS_GROUNDED = false;

            animator.SetBool("IsJumping", true);
        }

        horizontalInput = Input.GetAxisRaw("Horizontal"); // Player Controller [A & D Keyboard]
        rb2d.velocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, rb2d.velocity.y); // Player Move

        FlipPlayer();


        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        
        if (Input.GetKey(KeyCode.Space) && IS_GROUNDED) // Jump if Space bar is Pressed and Player is on Ground
        {
            JumpMovement();
        }
    }

    // For Mobile
    public void JumpMovement()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    public void FlipPlayer()
    {
        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
