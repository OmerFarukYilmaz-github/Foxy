using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;        // Kmaeranýn sadece oyuncuyu deðil baþka þeyleri de takip etmseini isteyebiliriz.
                                    // Bu yuzden en temel þeylerden biri transformu yani konumu seçiyoruz

    public Transform farBG, middleBG; // Backgroundlar

    public float minHeight, maxHeight;  // kamera en fazla ne kadar aþagý ve yukarý gitsin

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
            //M'ye basýlý tutarken
            if (Input.GetKey(KeyCode.M))
            {
                shouldFollowTarget = false;
                Camera.main.orthographicSize = 35;
                transform.position = new Vector3(24, -20, -10);
            }
            //M'yi býraktýðýnda
            if(Input.GetKeyUp(KeyCode.M))
            {
                shouldFollowTarget = true;
                Camera.main.orthographicSize = 10;
            }

        }

        if (shouldFollowTarget)     
        {
            //clampedY verilen deðerin(transform.position.y), verilen iki deðerin arasýnda kalmasýný saðlar
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);


           Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            // arkaplandaki resimler düz durmasýn canlý gibi görünsün diye farBG bizimle ayný hýzda
            // middleBG biraz daha yavaþ olacak þekilde hareket ettiriyoruz.
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
