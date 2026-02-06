using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per 50x frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 ||
            horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();

        }

        //Animation for running
        anim.SetFloat("horizontal", Mathf.Abs(horizontal)); //value must always be a positive value 
        anim.SetFloat("verticle", Mathf.Abs(vertical));     // <0.1 = no movement  , >0.1 = movement

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
    }

    //Used to flip Player
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
