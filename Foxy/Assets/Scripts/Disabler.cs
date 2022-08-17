using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject disable;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            disable.SetActive(false);
        }
    }
}
