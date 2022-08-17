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
        checkpoints = FindObjectsOfType<Checkpoint>(); //hiyerar�ideki t�m Checkpointler bulunup diziye atan�cak 

        spawnPoint = PlayerController.instance.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeActivateCheckpoints()
    {
       for(int i=0; i<checkpoints.Length; i++)        //dizideki t�m chekpointler geziliy�or
       {
            checkpoints[i].ResetCheckpoint();         //checkpointi off yap�yoruz (sprite de�i�iyor)
       }

    }

    public void SetSpawnPoint(Vector3 newSP)
    {
        spawnPoint = newSP;
    }


}
