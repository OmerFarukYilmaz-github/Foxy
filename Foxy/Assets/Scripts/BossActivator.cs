using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{

    public GameObject bossBattle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("Boss Active");
            bossBattle.SetActive(true);

            this.gameObject.SetActive(false);

            AudioManager.instance.PlayBossMusic();

        }
        
    }
}
