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
        if(other.tag=="Player" && !isCollected) //toplanabilir nesneye oyuncu de�erse ve daha �nce toplanmam��sa
        {                                       // bunun nedeni buglar�n �n�ne ge�me 2 tane saymas�n diye
                                                // mesela oyuncunun 2 collider� varsa
            if(isGem)
            {
                LevelManager.instance.gemsCollected++;      // gem say�s�n� 1 artt�r
                isCollected = true;

                Destroy(gameObject);       //nesneyi yok et

                Instantiate(pickupEffect, transform.position, transform.rotation);  // kopyas�n� olu�tur ve �al��t�r
                AudioManager.instance.PlaySFX(6);

                UIController.instance.UpdateGemCount();

            }

            if(isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();   // can� 1 artt�r

                    isCollected = true;
                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation);  // kopyas�n� olu�tur ve �al��t�r
                    AudioManager.instance.PlaySFX(7);
                }
            }


        }

    }
     
}
