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
    private bool isDead = false;
    private Rigidbody2D rb;
    private ScoreManager scoreManager;
    private JetpackHeatMeter heatMeter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        scoreManager = FindAnyObjectByType<ScoreManager>();

        heatMeter = FindAnyObjectByType<JetpackHeatMeter>();
    }

    void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        bool wantsThrust = Input.GetButton("Jump") || Input.GetMouseButton(0);

        if (wantsThrust)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, thrustForce);
            if (jetpackFire != null) jetpackFire.SetActive(true);
        }
        else
        {
            if (jetpackFire != null) jetpackFire.SetActive(false);
        }

        if (heatMeter != null) heatMeter.UpdateHeat(wantsThrust, this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && !isDead)
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (isDead) return;
        isDead = true;
        if (scoreManager != null) scoreManager.TriggerGameOver();
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