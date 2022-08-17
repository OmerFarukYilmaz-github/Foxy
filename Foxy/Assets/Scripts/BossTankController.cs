using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    // bir zamanda yaln�z bir durum olabilir
    public enum bossStates
    {
        shooting, 
        hurt,
        moving
    };
    public bossStates currentState;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]        //unityde okunulurlu�u artt�r�yor
    public float movementSpeed;
    public Transform leftPoint, rightPoint;
    private bool isMovingRight;
    public GameObject mine;
    public Transform minePoint;
    public float mineCooldown;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float shootCooldown;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]
    public int health=5;
    public GameObject explosion;
    public GameObject winPlatform;
    private bool isDefeated;
    public float shotSpeedUp;
    public float mineSpeedUp;

    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.shooting; //0 da diyebilirsin
    }

    // Update is called once per frame
    void Update()
    {
         switch(currentState)
         {
                case bossStates.shooting:

                shotCounter -= Time.deltaTime;

                if(shotCounter <=0)
                {
                    shotCounter = shootCooldown;

                    // bullet nesnesini firepoint'in oldu�u yerden onun y�n�nde olu�tur ve newBullet'e ata
                    // ��nk� isted�imiz y�nde gitmesini sa�layabilmek i�in bossun y��n� vermeliyiz
                    // bossun y�n� de�i�irken firePoints de�i�miyor?
                   var newBullet =  Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }

                    break;

                case bossStates.hurt:

                if(hurtCounter > 0)
                {
                        hurtCounter -= Time.deltaTime;

                    if(hurtCounter <= 0)
                    {
                        if (isDefeated)
                        {
                            Destroy(gameObject);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            winPlatform.SetActive(true);
                            AudioManager.instance.StopBossMusic();
                        }
                        else
                        {
                            currentState = bossStates.moving;

                            mineCounter = 0;
                        }
                    }
                }

                     break;

                case bossStates.moving:
                    
                if(isMovingRight)
                {

                    theBoss.position += new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        //sola giderken y�n� de�i�sin diye
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        isMovingRight = false;
                        EndMovement();
                    }
                    
                }
                else
                {
                    theBoss.position -= new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        //sa�a giderken y�n� de�i�sin diye
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        isMovingRight = true;

                        EndMovement();
                    }

                }

                mineCounter -= Time.deltaTime;
                if(mineCounter<0)
                {
                    mineCounter = mineCooldown;

                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                    break;

         }

        // UNITY_EDITOR sadece unityde �al��t�r�labilir
        // !UNITY_EDITOR sadece buildten sonra �al��t�r�labilir
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif

    }


    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");

        AudioManager.instance.PlaySFX(0);

        health--;
        if(health <= 0 )
        {
            isDefeated = true;
        }
        else
        {
            // s�re k�salaca�� i�in daha cok s�k�p daha cok may�n b�rakacak
            shootCooldown /= shotSpeedUp;
            mineCooldown /= mineSpeedUp;
        }

    }

    private void EndMovement()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }


}
