using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    public static FallingPlatformController instance;
    private GameObject[] fallingPlatformRespawnPoints;
    public GameObject fallingPlatform;


    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        fallingPlatformRespawnPoints = GameObject.FindGameObjectsWithTag("FallingPlatformRespawnPoint");
    }
    public void RespawnFallingPlatforms()
    {
      //  int i = 0;

        foreach (GameObject rPoint in fallingPlatformRespawnPoints)
        {
            if(rPoint.transform.parent== null)
            {
                //Debug.Log(i+". fall platform yok olmuþ");
                Instantiate(fallingPlatform, rPoint.transform.position, rPoint.transform.rotation);
            }
          /*  else
            {
               // Debug.Log(i + ". fall platform yerinde ");
            }
           // i++;*/
        }
    }


}
