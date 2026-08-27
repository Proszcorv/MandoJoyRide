using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [Tooltip("1 = az alapsebességgel (közeli háttérrel) egyezik. Pl. TIE fighter: 1.3, szikla: 0.9")]
    [SerializeField] private float speedMultiplier = 1f;

    private float leftBound = -15f;

    void Update()
    {
        float speed = GameSpeedManager.Instance.CurrentBaseSpeed * speedMultiplier;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}