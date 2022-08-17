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
            CheckpointController.instance.DeActivateCheckpoints();      // tüm checkpointleri kapatıyoruz
            spriteRenderer.sprite = cpOn;                               // ve en son geçtiğimiz yani şuankini aktif yapıyoruz
            CheckpointController.instance.SetSpawnPoint(transform.position);    //bulunduğumuz aktifleştirdiğimiz spawnpointin konummunu gönderiyoruz
        
        }

    }

    public void ResetCheckpoint()
    {
        spriteRenderer.sprite = cpOff;
    }


}
