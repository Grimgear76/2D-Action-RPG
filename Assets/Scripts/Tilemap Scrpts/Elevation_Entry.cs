using UnityEngine;
using System.Collections;


public class Elevation_Entry : MonoBehaviour
{

    public Collider2D[] mountainColliders;
    public Collider2D[] boundryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;
            }

            foreach (Collider2D boundry in boundryColliders)
            {
                boundry.enabled = true;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;

        }
    }




}
