using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Tooltip("1 = pont az alapsebességgel megy (közeli réteg). Kisebb érték = lassabb, távolabbi réteg (parallax).")]
    [SerializeField] private float speedMultiplier = 1f;

    private Vector3 startPosition;
    private float repeatWidth;

    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x;
    }

    void Update()
    {
        float speed = GameSpeedManager.Instance.CurrentBaseSpeed * speedMultiplier;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
    }
}