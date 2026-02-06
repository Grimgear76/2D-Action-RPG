using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }

}
