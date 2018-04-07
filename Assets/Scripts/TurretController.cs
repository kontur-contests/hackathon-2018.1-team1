using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    public GameObject bulletPrefab;
    public float shootInterval = 3f;
    public Vector3 bulletForceVector;
    public float lookupDistance = 100f;

    private int targetLayerMask = -1;

    void Start () {
        targetLayerMask = LayerMask.GetMask("Player");
        StartCoroutine(ShootTimer());
	}

    private bool PlayerIsOnAttackLine()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, bulletForceVector - transform.position, lookupDistance, targetLayerMask);

        return (hit.collider != null);
    }

    private IEnumerator ShootTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            if (PlayerIsOnAttackLine())
            {
                var bullet = Instantiate(bulletPrefab);
                var position = transform.position;
                position.z = 1;
                bullet.transform.position = position;
                var body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(bulletForceVector);
            }
        }
    }
}
