using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHealthController : MonoBehaviour
{
    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer spriteRenderer;
    public GameObject deathEffect;

    RobotController robotCtrl;
    GameObject robot;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;    // saniyede 60 �a�r�l�rsa hata olur onun yerine pfs/zaman a gore �al���l�r.
                                                    // Saniyede 1 kez �al���cak

            if (invincibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }
    public void Kill()
    {
        currentHealth = 0;
        DealDamage();
    }
    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {

            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation); //�l�m efekti

                robotCtrl = this.GetComponent<RobotController>();
                robotCtrl.GiveControlBacktoPlayer();
                Destroy(gameObject);

            }
            else
            {


                robotCtrl = this.GetComponent<RobotController>();
                robotCtrl.KnockBack();
            

                invincibleCounter = invincibleLength;

                // Direk renke eri�ip de�iemezsin oyuzden yeni color tan�mlamak gerek.
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
                AudioManager.instance.PlaySFX(9);
            }


        }

    }

}
