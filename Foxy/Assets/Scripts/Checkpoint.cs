using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite cpOn, cpOff;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CheckpointController.instance.DeActivateCheckpoints();      // t�m checkpointleri kapat�yoruz
            spriteRenderer.sprite = cpOn;                               // ve en son ge�ti�imiz yani �uankini aktif yap�yoruz
            CheckpointController.instance.SetSpawnPoint(transform.position);    //bulundu�umuz aktifle�tirdi�imiz spawnpointin konummunu g�nderiyoruz
        
        }

    }

    public void ResetCheckpoint()
    {
        spriteRenderer.sprite = cpOff;
    }


}
