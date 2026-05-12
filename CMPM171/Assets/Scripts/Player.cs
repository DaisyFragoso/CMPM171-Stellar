using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Vector2 respawnPoint;
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    // public PuzzleUIManager puzzleUIManager;

    private Rigidbody2D rb;
    private bool isGrounded;
    public static bool dragDropDone = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        respawnPoint = rb.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (transform.position.y < -10f)
        {
            Respawn();
        }

        spriteRenderer.flipX = rb.linearVelocity.x < 0;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Respawn()
    {
        rb.position = respawnPoint;
        rb.linearVelocity = Vector2.zero;
    }

}