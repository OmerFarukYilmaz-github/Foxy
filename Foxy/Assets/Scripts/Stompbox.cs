using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{

    public GameObject deathEffect;
    public GameObject collectible;

    [Range(0,100)] public float chanceToDrop;       // girilecek sayý 100 den buyuk olmasýn

    public bool isRobot;
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
        if(other.tag== "Enemy")
        {
            other.transform.parent.gameObject.SetActive(false); // Deðdiðimz objenin parentini deaktif et. zaten childtta deaktif olacak
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);

            if (isRobot)
            {
                RaycastDeneme.instance.roboComponent.Bounce();
            }
            else
            {
                PlayerController.instance.Bounce();
            }
            

            SpawnGem(other);

            AudioManager.instance.PlaySFX(3);

        }
        else if (other.tag == "Mine")
        {
            float dropSelect = Random.Range(0, 100);

            SpawnGem(other);
        }

    }
    private void SpawnGem(Collider2D other)
    {
        float dropSelect = Random.Range(0, 100);

        if (dropSelect <= chanceToDrop)
        {
            Instantiate(collectible, other.transform.position, other.transform.rotation);
        }
    }

}
