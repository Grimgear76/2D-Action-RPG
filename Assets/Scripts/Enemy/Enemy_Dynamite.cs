using UnityEngine;

public class Enemy_Dynamite : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject DynamitePrefab;
    private Enemy_movement movement;
    private Enemy_Combat combat;
    //private float cooldown;

    void Start()
    {
        movement = GetComponent<Enemy_movement>();
        combat = GetComponent<Enemy_Combat>();
    }

    public void Throw()
    {
        // Player hasn't been detected yet, don't throw
        if (movement.player == null)
        {
            return;
        }

        Vector2 direction = (movement.player.position - launchPoint.position).normalized;
        Dynamite dynamite = Instantiate(DynamitePrefab, launchPoint.position, Quaternion.identity)
            .GetComponent<Dynamite>();
        dynamite.direction = direction;
        dynamite.Initialize(combat.damage);
    }
}