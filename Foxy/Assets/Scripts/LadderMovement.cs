using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float  speed=8f;
    private bool isLadder, isClimbing;

    private Rigidbody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if(isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        if (isClimbing)
        {
            rb2d.gravityScale = 0f;
            rb2d.velocity = new Vector2(rb2d.velocity.x, vertical * speed);
        }
        else
        {
            rb2d.gravityScale = 5f;
        }
        anim.SetBool("isClimbing", isClimbing);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Ladder")
        {
            isLadder = true;
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            isLadder = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            isLadder = false;
            isClimbing = false;
        }
    }

}
