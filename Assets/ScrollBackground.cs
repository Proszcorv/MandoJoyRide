using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Vector3 startPosition;
    private float repeatWidth;

    void Start()
    {
        startPosition = transform.position;
        repeatWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < startPosition.x - repeatWidth)
        {
            transform.position = startPosition;
        }
    }
}