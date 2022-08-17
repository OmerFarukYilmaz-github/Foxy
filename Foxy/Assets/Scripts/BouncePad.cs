using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{

    private Animator anim;

    public float bounceForce = 20;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"  )
        {
            PlayerController.instance.rb2D.velocity = new Vector2(PlayerController.instance.rb2D.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
        else if (other.tag == "Robot")
        {
            RaycastDeneme.instance.roboComponent.rb2D.velocity = new Vector2(RaycastDeneme.instance.roboComponent.rb2D.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
    }

}
