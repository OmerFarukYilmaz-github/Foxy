using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public static CheckpointController instance;

    private Checkpoint[] checkpoints;

    public Vector3 spawnPoint;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>(); //hiyerarþideki tüm Checkpointler bulunup diziye atanýcak 

        spawnPoint = PlayerController.instance.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeActivateCheckpoints()
    {
       for(int i=0; i<checkpoints.Length; i++)        //dizideki tüm chekpointler geziliyýor
       {
            checkpoints[i].ResetCheckpoint();         //checkpointi off yapýyoruz (sprite deðiþiyor)
       }

    }

    public void SetSpawnPoint(Vector3 newSP)
    {
        spawnPoint = newSP;
    }


}
