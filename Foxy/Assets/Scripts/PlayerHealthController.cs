using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;  // heryerden çaðrýrýlýp eriþilebilen bir sýnýf tanýmladýk
    public int currentHealth,maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer spriteRenderer;

    public GameObject deathEffect;

    public void Awake()
    {
        instance = this; // yaratýlan sýnýfýn bu sýnýf olmasýný saðladýk. yai instance þuan kodalarýný gördüðümüz sýnýfýn temsilciisi
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter >0)
        {
            invincibleCounter -= Time.deltaTime;    // saniyede 60 çaðrýlýrsa hata olur onun yerine pfs/zaman a gore çalýþýlýr.
                                                    // Saniyede 1 kez çalýþýcak

            if(invincibleCounter <=0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {

            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation); //ölüm efekti

                LevelManager.instance.RespawnPlayer();      //respwan ol

            }
            else
            {
                PlayerController.instance.KnockBack(); 
               

                invincibleCounter = invincibleLength;

                // Direk renke eriþip deðiemezsin oyuzden yeni color tanýmlamak gerek.
                spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, 0.5f);
                AudioManager.instance.PlaySFX(9);
            }

            
            UIController.instance.UpdateHealthDisplay();

        }

    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();

    }

}
