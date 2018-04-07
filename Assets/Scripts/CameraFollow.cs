using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        smoothedPosition.z = -10;

        transform.position = smoothedPosition;
    }
}
