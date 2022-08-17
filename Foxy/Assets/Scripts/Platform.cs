using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D platformEffector;
    public bool isTwoWay, isFallingDown;
    private Rigidbody2D rb2D;
    public float fallDelay= 1f, destroyDelay=2f;
    public Transform platformRespawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
        if (isFallingDown)
        {
            rb2D = GetComponent<Rigidbody2D>();
           // platformRespawnPoint.parent = null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isTwoWay)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Asagi ");
                platformEffector.rotationalOffset = 180;
            }
            if (PlayerController.instance.isOnPlatform == false)
            {
                platformEffector.rotationalOffset = 0;
            }
        }

  



    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" )
        {
            if (isFallingDown)
            {
                StartCoroutine(Fall());
            }
        }

    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        platformRespawnPoint.parent = null;
        Destroy(gameObject, destroyDelay);

    }



}
