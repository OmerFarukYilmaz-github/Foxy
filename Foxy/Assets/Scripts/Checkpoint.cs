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
            CheckpointController.instance.DeActivateCheckpoints();      // tüm checkpointleri kapatýyoruz
            spriteRenderer.sprite = cpOn;                               // ve en son geçtiðimiz yani þuankini aktif yapýyoruz
            CheckpointController.instance.SetSpawnPoint(transform.position);    //bulunduðumuz aktifleþtirdiðimiz spawnpointin konummunu gönderiyoruz
        
        }

    }

    public void ResetCheckpoint()
    {
        spriteRenderer.sprite = cpOff;
    }


}
