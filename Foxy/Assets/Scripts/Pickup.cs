using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem,isHeal;
    private bool isCollected;
    public GameObject pickupEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.tag=="Player" && !isCollected) //toplanabilir nesneye oyuncu deðerse ve daha önce toplanmamýþsa
        {                                       // bunun nedeni buglarýn önüne geçme 2 tane saymasýn diye
                                                // mesela oyuncunun 2 colliderý varsa
            if(isGem)
            {
                LevelManager.instance.gemsCollected++;      // gem sayýsýný 1 arttýr
                isCollected = true;

                Destroy(gameObject);       //nesneyi yok et

                Instantiate(pickupEffect, transform.position, transform.rotation);  // kopyasýný oluþtur ve çalýþtýr
                AudioManager.instance.PlaySFX(6);

                UIController.instance.UpdateGemCount();

            }

            if(isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();   // caný 1 arttýr

                    isCollected = true;
                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation);  // kopyasýný oluþtur ve çalýþtýr
                    AudioManager.instance.PlaySFX(7);
                }
            }


        }

    }
     
}
