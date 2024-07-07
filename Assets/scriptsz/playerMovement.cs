using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    public GameObject gameobject;
    public Vector3 scale;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() 
    {
        animator.SetFloat("horiSpeed", Mathf.Abs(movement.x));
        animator.SetFloat("vertSpeed", movement.y);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //check for up and down movement
        if(movement.y > 0.01)
        {
            animator.SetBool("isUp", true);
        }
        else if(movement.y < -0.01)
        {
            animator.SetBool("isDown", true);
        }
        else
        {
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", false);
        }

        //check for left and right movement
        if (movement.x > 0.01 || movement.x < -0.01)
        {
            animator.SetBool("isSide", true);
        }
        else
        {
            animator.SetBool("isSide", false);
        }

        if (movement.x > 0.01)
        {
            animator.SetBool("mirror", true);
        }
        if (movement.x < -0.01)
        {
            animator.SetBool("mirror", false);
        }
    }
}
