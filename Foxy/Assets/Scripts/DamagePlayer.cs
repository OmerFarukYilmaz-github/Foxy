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
        if(other.tag=="Player" ) //"Player" tagli bir nesne bu kodun bagl� oldugu nesnenin(spike) trigger�na girerse
        {

            Debug.Log("Hit!");
            //FindObjectOfType<PlayerHealthController>().DealDamage();
            //hiyerar�ideki nesneleri gezer "PlayerHealthController" scriptini arar. Scriptteki DealDamage  fonksiyonunu �a��r�r.
            //�ok nesne yoksa olabilir

            //Di�er yol ise PlayerHealthController'da bir tane static nesne tan�mlan�r ve sonra her yerden eri�ilebilir.
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
