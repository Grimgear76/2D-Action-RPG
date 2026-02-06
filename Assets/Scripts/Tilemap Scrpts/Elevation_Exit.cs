using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{

    public Collider2D[] mountainColliders;
    public Collider2D[] boundryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;
            }

            foreach (Collider2D boundry in boundryColliders)
            {
                boundry.enabled = false;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;

        }
    }

}
