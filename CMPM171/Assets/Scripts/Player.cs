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
    public int extraJumpsValue = 1;
    private int extraJumps;
    public static bool dragDropDone = false;

    public GameObject controlsUI;
    public GameObject jumpUI;

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
    
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded) 
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            } 
            else  if (extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (transform.position.y < -25f)
        {
            Respawn();
        }

        if (transform.position.x > -117f && transform.position.x < -109f)
        {
            controlsUI.SetActive(true);
        }
        else
        {
            controlsUI.SetActive(false);
        }
        
        if (transform.position.x > -101f && transform.position.x < -95f)
        {
            jumpUI.SetActive(true);
        }
        else
        {
            jumpUI.SetActive(false);
        }

        if (rb.linearVelocity.x == 0)
        {
            animator.SetFloat("Speed", 0);
        }
        else if (rb.linearVelocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.linearVelocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }

        if (rb.linearVelocity.y > 0 && !isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
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