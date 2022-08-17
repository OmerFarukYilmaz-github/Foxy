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
        rb2D = GetComponent<Rigidbody2D>();       // ba�l� oldu�u rigidbody
        anim = GetComponent<Animator>();

        //left point ve rigth point ba�ta d��man�n child objeleri .
        //enemy hareket etti�inde childobjelerde birlikte hareket edece�i i�in buga girecek ve sonsuza kadar ayn� yere gitmeye �al���cak
        //bu yuzden sol ve sag noktalar�n konumlar�n� al�p onlar� art�k child olmaktan c�kar�yoruz
        // ilk basta child yapma nedenimiz d�zen vee rahatl�k
        leftPoint.parent = null;
        rigthPoint.parent = null;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;        //geri say�m

            if (ismovingRight)       //saga gidiyorsak
            {
                rb2D.velocity = new Vector2(movementSpeed, rb2D.velocity.y);

                spriteRenderer.flipX = true;

                if (transform.position.x > rigthPoint.position.x)            //gidilebilecek en sa� noktaya vard�ysak
                {
                    ismovingRight = false;
                }

            }
            else
            {
                rb2D.velocity = new Vector2(-movementSpeed, rb2D.velocity.y);

                spriteRenderer.flipX = false;

                if (transform.position.x < leftPoint.position.x)            //gidilebilecek en sol noktaya vard�ysak
                {
                    ismovingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);   // bekleme zaman� rastgele olsun diye hep ayn� yerlerde beklemesin

            }

            if (isFrog) { anim.SetBool("isMoving", true); }//z�plama hareketi animasyonu icin
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;        //geri say�m
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * .75f, moveTime * .75f);

            }

            if (isFrog){anim.SetBool("isMoving", false); }  //z�plama hareketi animasyonu icin
        }


    }

}
