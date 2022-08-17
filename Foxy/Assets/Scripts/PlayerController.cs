using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;    //heryerden eri�ilebilsin

    public float movementSpeed=8f;     // hareket h�z� 7.5 olabilir

    [Header("Jump")]
    public float jumpForce=12f;         // z�plama g�c� gravity5 iken 15 olabilir �ift z�plama varken 12 iyidir.
    public float bounceForce = 15f;
    private bool canDoubleJump=true;
    public bool isGrounded;
    public bool isOnPlatform;

    public Transform groundCheckPoint;  // playera child oject yap�p, player�n alt�na bombo� bir nesne yerle�tir.Onun transformu
    public LayerMask whatIsGround;      // nereye de�di�i kontrol edilecek. Ground se�ilir. (Oyuncu ve zemin i�in layerlar olu�turduk)
    public LayerMask whatIsMainGround;

    [Header("Physic(will asign at start)")]
    public Rigidbody2D rb2D;                // playerin fizi�i
    public Animator anim;                   // playerin animasyonlar�
    private SpriteRenderer spriteRenderer;   // playerin goruntusu
    public bool isFacingRight;

    [Header("Knockback")]
    public float knockBackLength=0.25f;
    public float knockBackForce = 5f;
    private float knockBackCounter;
    private bool isHurt;

    [Header("Stop Input")]
    public bool stopInput;

    [Header("Restriction")]
    public bool isLeftRestricted;
    public bool isRightRestricted;
    public bool isJumpRestricted;



    public void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();   // oyuncunun inspector sekmesinde olan Animatoru al�cak. senin s�r�kle b�rak yapmana gerek yok.
        spriteRenderer = GetComponent<SpriteRenderer>();    //Sprite Renderer i alacak
    }

    // Update is called once per frame  u
    void Update()
    {

        #region Hareket ve Z�plama

        // ya oyuncu olacak; pause ve input durdurma olmayacak
        // ya robot olacak; aktif olacak
        if (PauseMenu.instance.isPaused==false && stopInput == false)        //pause ekran�nda tu�a bas�l�rsa unpause dan sonra o hareket yap�l�r.O yuzden pause de�ilse hareket et
        {
            if (knockBackCounter <= 0)      // knockback s�ras�nda hareket edilemesin diye
            {
                isHurt = false;
                rb2D.velocity = new Vector2(movementSpeed * Input.GetAxisRaw("Horizontal"), rb2D.velocity.y);    // x de h�z� de�i� y'de sabit kals�n

                #region Sag-sol hareket
                if (rb2D.velocity.x > 0)
                {
                    Debug.Log("sa�a gitmek isteniyor");

                    //Sa�a gitmek isteniyor ama aktif de�il
                    if (isRightRestricted)
                    {
                        Debug.Log("sa� aktif degil. ");
                        rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
                    }
                }
                else if (rb2D.velocity.x < 0)
                {
                    Debug.Log("sola gitmek isteniyor");

                    //sola gitmek isteniyor ama aktif de�il
                    if (isLeftRestricted)
                    {
                        Debug.Log("sol aktif degil. ");
                        rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
                    }
                }
                else
                {
                    // bisey yapma
                }
                #endregion                                                                                            // GetAxis te tusu b�rakt�ktan sonra da momentumdan dolay� hareket olur. GetAxisRaw yaparsan olmaz

                #region Z�plama
               
                    //whatIsGround olan katmman(zemin), .2f �ap i�erisinde groundCheckPoint(karakterin aya��ndaki bo� nesne) noktas�na temas ediyorsa true d�ner.
                    isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                    if (isGrounded)      // zemindeysen �ift z�plama m�mk�n hale getir
                    {
                        canDoubleJump = true;
                    }
                if (!isJumpRestricted)
                {
                    if (Input.GetButtonDown("Jump"))  // get buttondown bas�l�nca - get button bas�l� tutulu iken - get buttonup basma b�rak�ld���nda
                    {
                        if (isGrounded)// zemindeysek z�playa biliriz
                        {
                            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce); // z�pla
                            AudioManager.instance.PlaySFX(10);
                        }
                        else
                        {
                            if (canDoubleJump)   //zeminde de�iliz ama �ift z�plma m�mk�n
                            {
                                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce); // z�pla
                                canDoubleJump = false;  //2.kez z�plad�n yeter �ift z�plama zemine inip aktif olana kadar kapal�
                                AudioManager.instance.PlaySFX(10);
                            }
                        }
                    }
                }
                else { Debug.Log("Z�plama aktif degil."); }
                #endregion

                #region Sprite Yonu Degisme
                if (rb2D.velocity.x < 0)              //h�z 0'dan d���kken sola gidiyor demektir �yleyse sola d�nd�r.
                {
                    spriteRenderer.flipX = true;     //g�r�nt�y� sola �evir
                    isFacingRight = false;
                }
                else if (rb2D.velocity.x > 0)         //h�z 0'dan d���kken sa�a gidiyor demektir �yleyse sa�a d�nd�r.
                {
                    spriteRenderer.flipX = false;    //g�r�nt�y� sa�a �evir
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


        anim.SetFloat("movementSpeed", Mathf.Abs(rb2D.velocity.x));         //animatordeki "movementSpeed" isimli float� player�n h�z�na e�itle
                                                                 //ama ters yonde giderken h�z eksi olacak ve animatorde h�z>0 i�in
                                                                 //run aninmasyonu �al��s�n dedi�imiz i�in �al��mayacak o yuzden mutlak h�z� g�ndericez 
      
        anim.SetBool("isGrounded", isGrounded);                  //animatordeki "isGreounded" isimli boolu bu scriptteki isGrounded'a ez�h�itle

        anim.SetBool("isHurt", isHurt);
    }

 
    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        rb2D.velocity = new Vector2(0f, knockBackForce);
    }

    public  void Bounce()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }

  

    public void OnCollisionEnter2D(Collision2D other)
    {
        // platforma ��kt���m�zda platform hareket ederken oyuncu sabit kal�yor ve platform alt�ndan kaym�� gibi oluy�r
        // burda oyuncu platforma ��kt���nda platform ve oyuncu parent-child oluyor ki platformla beraber oyuncu da hareket edebilsin
        if(other.gameObject.tag=="Platform")
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

}
