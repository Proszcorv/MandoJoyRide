using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float lifetime = 1f;

    void Start()
    {
        StartCoroutine(DestroyAfterRealSeconds());
    }

    IEnumerator DestroyAfterRealSeconds()
    {
        yield return new WaitForSecondsRealtime(lifetime);
        Destroy(gameObject);
    }
}