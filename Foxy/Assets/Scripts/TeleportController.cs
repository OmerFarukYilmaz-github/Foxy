using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    private GameObject currentTeleporter;
    bool isTeleporter;

 
    // Update is called once per frame
    void Update()
    {
        if(isTeleporter && currentTeleporter != null)
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().destination.position;
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Teleporter")
        {
            Debug.Log("TELEPORTER enter");
            currentTeleporter = other.gameObject;
            isTeleporter = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Teleporter")
        {
            Debug.Log("TELEPORTER stay");
            currentTeleporter = other.gameObject;
            isTeleporter = true;
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Teleporter")
        {
            Debug.Log("TELEPORTER exit");
            currentTeleporter = null;
            isTeleporter = false;
        }
    }

}
