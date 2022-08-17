using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject objectToSwitch;
    private SpriteRenderer spriteRenderer;
    public Sprite downSprite;
    private bool hasSwitch;
    public bool deactivateOnSwitch;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if((other.tag=="Player" || other.tag=="Robot") && !hasSwitch)
        {
            
            if(deactivateOnSwitch)
            {

                objectToSwitch.SetActive(false);
            }
            else
            {

                objectToSwitch.SetActive(true);
            }
            
            spriteRenderer.sprite = downSprite;
            hasSwitch = true;


        }


    }





}
