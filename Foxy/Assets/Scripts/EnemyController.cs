using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isFrog;

    public float movementSpeed, moveTime, waitTime;
    private float moveCount, waitCount;

    private bool ismovingRight=true;

    public Transform leftPoint, rigthPoint;
    private Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();       // baðlý olduðu rigidbody
        anim = GetComponent<Animator>();

        //left point ve rigth point baþta düþmanýn child objeleri .
        //enemy hareket ettiðinde childobjelerde birlikte hareket edeceði için buga girecek ve sonsuza kadar ayný yere gitmeye çalýþýcak
        //bu yuzden sol ve sag noktalarýn konumlarýný alýp onlarý artýk child olmaktan cýkarýyoruz
        // ilk basta child yapma nedenimiz düzen vee rahatlýk
        leftPoint.parent = null;
        rigthPoint.parent = null;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;        //geri sayým

            if (ismovingRight)       //saga gidiyorsak
            {
                rb2D.velocity = new Vector2(movementSpeed, rb2D.velocity.y);

                spriteRenderer.flipX = true;

                if (transform.position.x > rigthPoint.position.x)            //gidilebilecek en sað noktaya vardýysak
                {
                    ismovingRight = false;
                }

            }
            else
            {
                rb2D.velocity = new Vector2(-movementSpeed, rb2D.velocity.y);

                spriteRenderer.flipX = false;

                if (transform.position.x < leftPoint.position.x)            //gidilebilecek en sol noktaya vardýysak
                {
                    ismovingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);   // bekleme zamaný rastgele olsun diye hep ayný yerlerde beklemesin

            }

            if (isFrog) { anim.SetBool("isMoving", true); }//zýplama hareketi animasyonu icin
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;        //geri sayým
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * .75f, moveTime * .75f);

            }

            if (isFrog){anim.SetBool("isMoving", false); }  //zýplama hareketi animasyonu icin
        }


    }

}
