using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float wallJumpTime = 0.2f, wallSlideSpeed = 0.3f, wallDistance= 0.51f;
    bool isWallSliding;
    RaycastHit2D wallCheckHit;
    float jumpTime;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (rb2D.velocity.x > 0)    // saga dogru          
        {
            // temas kontrol ediyor true yada false donucek
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0f),wallDistance, PlayerController.instance.whatIsGround);
        }
        else     //sola dogru    
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0f), wallDistance, PlayerController.instance.whatIsGround);

        }

        // yerde deðiliz ve hareket etmeye calisiyoruz
        if (wallCheckHit && !PlayerController.instance.isGrounded && Input.GetAxis("Horizontal")!=0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if(jumpTime < Time.time)
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {

            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, wallSlideSpeed, float.MaxValue));
        }


        


    }
    public void Update()
    {
        if (isWallSliding && Input.GetButtonDown("Jump"))
        { 
            rb2D.velocity = new Vector2(rb2D.velocity.x, PlayerController.instance.jumpForce); // zýpla
        }
    }



}
