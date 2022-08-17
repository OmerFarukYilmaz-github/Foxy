using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public static RobotController instance;
	public bool isRobotActive;

    public float movementSpeed = 8f;     // hareket hýzý 7.5 olabilir

    [Header("Jump")]
    public float jumpForce = 12f;         // zýplama gücü gravity5 iken 15 olabilir çift zýplama varken 12 iyidir.
    public float bounceForce = 15f;
    private bool canDoubleJump = true;
    public bool isGrounded;
    public bool isOnPlatform;

    public Transform groundCheckPoint;  // playera child oject yapýp, playerýn altýna bomboþ bir nesne yerleþtir.Onun transformu
    public LayerMask whatIsGround;      // nereye deðdiði kontrol edilecek. Ground seçilir. (Oyuncu ve zemin için layerlar oluþturduk)



    [Header("Physic(will asign at start)")]
    public Rigidbody2D rb2D;                // playerin fiziði
    public Animator anim;                   // playerin animasyonlarý
    private SpriteRenderer spriteRenderer;   // playerin goruntusu
    public bool isFacingRight;


    [Header("Knockback")]
    public float knockBackLength = 0.25f;
    public float knockBackForce = 5f;
    private float knockBackCounter;
    private bool isHurt;


    public GameObject robots;
    public Transform robotRespawnPoint;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   // oyuncunun inspector sekmesinde olan Animatoru alýcak. senin sürükle býrak yapmana gerek yok.
        spriteRenderer = GetComponent<SpriteRenderer>();    //Sprite Renderer i alacak
        robotRespawnPoint.parent = null;
    }

    // Update is called once per frame  u
    void Update()
    {

        #region Hareket ve Zýplama

        // ya oyuncu olacak; pause ve input durdurma olmayacak
        // ya robot olacak; aktif olacak
        if (PauseMenu.instance.isPaused == false && isRobotActive)        //pause ekranýnda tuþa basýlýrsa unpause dan sonra o hareket yapýlýr.O yuzden pause deðilse hareket et
        {
            if (knockBackCounter <= 0)      // knockback sýrasýnda hareket edilemesin diye
            {
                isHurt = false;
                rb2D.velocity = new Vector2(movementSpeed * Input.GetAxisRaw("Horizontal"), rb2D.velocity.y);    // x de hýzý deðiþ y'de sabit kalsýn

            

                #region Zýplama
                
                    //whatIsGround olan katmman(zemin), .2f çap içerisinde groundCheckPoint(karakterin ayaðýndaki boþ nesne) noktasýna temas ediyorsa true döner.
                    isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                    if (isGrounded)      // zemindeysen çift zýplama mümkün hale getir
                    {
                        canDoubleJump = true;
                    }

                    if (Input.GetButtonDown("Jump"))  // get buttondown basýlýnca - get button basýlý tutulu iken - get buttonup basma býrakýldýðýnda
                    {
                        if (isGrounded)// zemindeysek zýplaya biliriz
                        {
                            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce); // zýpla
                            AudioManager.instance.PlaySFX(10);
                        }
                        else
                        {
                            if (canDoubleJump)   //zeminde deðiliz ama çift zýplma mümkün
                            {
                                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce); // zýpla
                                canDoubleJump = false;  //2.kez zýpladýn yeter çift zýplama zemine inip aktif olana kadar kapalý
                                AudioManager.instance.PlaySFX(10);
                            }
                        }
                    }
               
                else { Debug.Log("Zýplama aktif degil."); }
                #endregion

                #region Sprite Yonu Degisme
                if (rb2D.velocity.x < 0)              //hýz 0'dan düþükken sola gidiyor demektir öyleyse sola döndür.
                {
                    spriteRenderer.flipX = true;     //görüntüyü sola çevir
                    isFacingRight = false;
                }
                else if (rb2D.velocity.x > 0)         //hýz 0'dan düþükken saða gidiyor demektir öyleyse saða döndür.
                {
                    spriteRenderer.flipX = false;    //görüntüyü saða çevir
                    isFacingRight = true;
                }
                #endregion

            }
            else
            {
                isHurt = true;
                knockBackCounter -= Time.deltaTime;

                if (spriteRenderer.flipX == true)
                {
                    rb2D.velocity = new Vector2(knockBackForce, rb2D.velocity.y);
                }
                else
                {
                    rb2D.velocity = new Vector2(-knockBackForce, rb2D.velocity.y);
                }

            }
        }

        #endregion

        if (Input.GetKeyDown(KeyCode.B))
        {
            GiveControlBacktoPlayer();

        }

        anim.SetFloat("movementSpeed", Mathf.Abs(rb2D.velocity.x));         //animatordeki "movementSpeed" isimli floatý playerýn hýzýna eþitle
                                                                            //ama ters yonde giderken hýz eksi olacak ve animatorde hýz>0 için
                                                                            //run aninmasyonu çalýþsýn dediðimiz için çalýþmayacak o yuzden mutlak hýzý göndericez 

        anim.SetBool("isGrounded", isGrounded);                  //animatordeki "isGreounded" isimli boolu bu scriptteki isGrounded'a ezýhþitle

        anim.SetBool("isHurt", isHurt);





    }


    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        rb2D.velocity = new Vector2(0f, knockBackForce);
    }

    public void Bounce()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        // platforma çýktýðýmýzda platform hareket ederken oyuncu sabit kalýyor ve platform altýndan kaymýþ gibi oluyýr
        // burda oyuncu platforma çýktýðýnda platform ve oyuncu parent-child oluyor ki platformla beraber oyuncu da hareket edebilsin
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("Platforma girdik");
            transform.parent = other.transform;
            isOnPlatform = true;
        }

    }
    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isOnPlatform = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("Platformdan ciktik");
            transform.parent = null;
            isOnPlatform = false;
        }

    }

    public void GiveControlBacktoPlayer()
    {
        RaycastDeneme.instance.isPossessed = false;
        RaycastDeneme.instance.isPlayer = true;
        PlayerController.instance.stopInput = false;
        isRobotActive = false;
        CameraController.instance.target = PlayerController.instance.transform;
        PlayerController.instance.rb2D.bodyType = RigidbodyType2D.Dynamic;
        this.rb2D.bodyType = RigidbodyType2D.Static;
    }

}

