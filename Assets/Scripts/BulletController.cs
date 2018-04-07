using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float destroyTime = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Camera.main.GetComponent<GameManager>().TakeDamage(30);

        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterTimeout());
    }

    private IEnumerator DestroyAfterTimeout()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
