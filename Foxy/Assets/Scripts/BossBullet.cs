using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(2);
    }

    // Update is called once per frame
    void Update()
    {

        //speed * transform.localScale.x   Döndüðümüz yöne göre 1 yada -1 gelecek ve bunu hýzla çarparak
        //kurþunun hangi yöne doðru ilerlemesi gerekitiðini belirtiyoruz
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);    
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamage();
           
            
        }
        AudioManager.instance.PlaySFX(1);
        Destroy(gameObject);        // herhangi bir þeye çarparsa yok et
    }



}
