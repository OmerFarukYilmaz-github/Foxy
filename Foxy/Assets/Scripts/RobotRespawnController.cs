using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRespawnController : MonoBehaviour
{
    public static RobotRespawnController instance;
    private GameObject[] robotRespawnPoints, robots;
    public GameObject robot;


    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        robotRespawnPoints = GameObject.FindGameObjectsWithTag("robotRespawnPoints");
        
    }
    public void RespawnDestroyedRobots()
    {
        robots = GameObject.FindGameObjectsWithTag("Robot");
        
        // tüm robotlarý yok et
        foreach(GameObject robot in robots)
        {
            Destroy(robot);
        }

        // hepsinin yerine yenisini koy.
        foreach (GameObject rPoint in robotRespawnPoints)
        {
           
             Instantiate(robot, rPoint.transform.position, rPoint.transform.rotation);
        
        }
    }
}
