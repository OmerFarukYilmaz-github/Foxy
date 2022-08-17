using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;
    public Transform platform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //                                          bas                     gidilecek                       h�z
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed*Time.deltaTime);
        
        // platform gidilecek bi noktaya vard���nda orda tak�l� kalmas�n yonunu de�i�sin diye
        if(Vector3.Distance(platform.position, points[currentPoint].position) <.05f)
        {
            currentPoint++;
        }
        currentPoint %= points.Length;

    }


}
