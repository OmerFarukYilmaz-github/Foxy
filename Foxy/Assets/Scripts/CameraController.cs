using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;        // Kmaeran�n sadece oyuncuyu de�il ba�ka �eyleri de takip etmseini isteyebiliriz.
                                    // Bu yuzden en temel �eylerden biri transformu yani konumu se�iyoruz

    public Transform farBG, middleBG; // Backgroundlar

    public float minHeight, maxHeight;  // kamera en fazla ne kadar a�ag� ve yukar� gitsin

    private Vector2 lastPos;

    public bool shouldFollowTarget=true;

    public bool isCave, isMaze;


    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMaze)
        {
            //M'ye bas�l� tutarken
            if (Input.GetKey(KeyCode.M))
            {
                shouldFollowTarget = false;
                Camera.main.orthographicSize = 35;
                transform.position = new Vector3(24, -20, -10);
            }
            //M'yi b�rakt���nda
            if(Input.GetKeyUp(KeyCode.M))
            {
                shouldFollowTarget = true;
                Camera.main.orthographicSize = 10;
            }

        }

        if (shouldFollowTarget)     
        {
            //clampedY verilen de�erin(transform.position.y), verilen iki de�erin aras�nda kalmas�n� sa�lar
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);


           Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            // arkaplandaki resimler d�z durmas�n canl� gibi g�r�ns�n diye farBG bizimle ayn� h�zda
            // middleBG biraz daha yava� olacak �ekilde hareket ettiriyoruz.
            farBG.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBG.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;

            lastPos = transform.position;
        }

       
    }

    public void SetCameraSize(int size)
    {
        Camera.main.orthographicSize = size;
    
    }
}
