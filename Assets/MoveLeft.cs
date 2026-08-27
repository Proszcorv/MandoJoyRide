using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float accelerationRate = 0.05f;
    [SerializeField] private float maxSpeed = 10f;

    private float leftBound = -15f;

    void Start()
    {
        
    }

    void Update()
    {
        if (speed < maxSpeed)
        {
            speed += accelerationRate * Time.deltaTime;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
