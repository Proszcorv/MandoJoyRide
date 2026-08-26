using UnityEngine;
using UnityEngine.SceneManagement;

public class JetpackController : MonoBehaviour
{
    [SerializeField] private float thrustForce = 5f;

    [SerializeField] private GameObject jetpackFire;

    [Header("Animáció és Talaj érzékelés")]
    public Animator animator;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; 

    private bool isGrounded;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (Input.GetButton("Jump") || Input.GetMouseButton(0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, thrustForce);

            if (jetpackFire != null)
            {
                jetpackFire.SetActive(true);
            }
        }
        else
        {
            if (jetpackFire != null)
            {
                jetpackFire.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}