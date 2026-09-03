using UnityEngine;
using UnityEngine.SceneManagement;

public class JetpackController : MonoBehaviour
{
    [SerializeField] private float thrustForce = 5f;

    [SerializeField] private GameObject jetpackFire;

    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private Transform explosionSpawnPoint;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioSource jetpackAudioSource;

    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioSource sfxAudioSource;

    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private float footstepInterval = 0.3f;

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
    private float footstepTimer = 0f;

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

        if (isGrounded && !isDead)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                footstepTimer = 0f;
                if (footstepAudioSource != null && footstepSound != null)
                {
                    footstepAudioSource.PlayOneShot(footstepSound);
                }
            }
        }
        else
        {
            footstepTimer = 0f; 
        }

        bool wantsThrust = Input.GetButton("Jump") || Input.GetMouseButton(0);

        if (wantsThrust)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, thrustForce);
            if (jetpackFire != null) jetpackFire.SetActive(true);

            if (jetpackAudioSource != null && !jetpackAudioSource.isPlaying)
            {
                jetpackAudioSource.Play();
            }
        }
        else
        {
            if (jetpackFire != null) jetpackFire.SetActive(false);

            if (jetpackAudioSource != null && jetpackAudioSource.isPlaying)
            {
                jetpackAudioSource.Stop();
            }
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

        if (jetpackAudioSource != null) jetpackAudioSource.Stop();
        if (footstepAudioSource != null) footstepAudioSource.Stop();

        if (explosionPrefab != null)
        {
            Vector3 spawnPos = explosionSpawnPoint != null ? explosionSpawnPoint.position : transform.position;
            Instantiate(explosionPrefab, spawnPos, Quaternion.identity);
        }

        if (sfxAudioSource != null && explosionSound != null)
        {
            sfxAudioSource.PlayOneShot(explosionSound);
        }

        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (jetpackFire != null) jetpackFire.SetActive(false);

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