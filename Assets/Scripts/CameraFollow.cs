using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Assign your player in Inspector
    public Vector3 offset;     // Distance from the player

    void Start()
    {
        // Optional: set default offset
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Directly follow the player
        transform.position = player.position + offset;
    }
}
