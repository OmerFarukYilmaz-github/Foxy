using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private PlayerHealthController  playerHealth;
    private RobotHealthController roboHealth;

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
        if(other.tag=="Player" ) //"Player" tagli bir nesne bu kodun baglý oldugu nesnenin(spike) triggerýna girerse
        {

            Debug.Log("Hit!");
            //FindObjectOfType<PlayerHealthController>().DealDamage();
            //hiyerarþideki nesneleri gezer "PlayerHealthController" scriptini arar. Scriptteki DealDamage  fonksiyonunu çaðýrýr.
            //çok nesne yoksa olabilir

            //Diðer yol ise PlayerHealthController'da bir tane static nesne tanýmlanýr ve sonra her yerden eriþilebilir.
          playerHealth= other.GetComponent<PlayerHealthController>();
            playerHealth.DealDamage();

        }
        else if (other.tag == "Robot") 
        {
            Debug.Log("Robot Hit!");
            roboHealth = other.GetComponent<RobotHealthController>();
            roboHealth.DealDamage();
        }
    }



}
