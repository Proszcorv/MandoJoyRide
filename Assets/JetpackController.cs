using UnityEngine;
using UnityEngine.SceneManagement;

public class JetpackController : MonoBehaviour
{
    [SerializeField] private float thrustForce = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButton("Jump") || Input.GetMouseButton(0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, thrustForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}