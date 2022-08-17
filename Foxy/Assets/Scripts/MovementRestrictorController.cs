using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRestrictorController : MonoBehaviour
{
    public bool shouldRestrictRight, shouldRestrictLeft, shouldRestrictJump;
    public float countDown;
    private float countDownTime=0, countDownTimer;


    // Start is called before the first frame update
    void Start()
    {
        countDownTimer = countDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        // 0 ise hareket kýsýtlama hep sürer
        // yoksa bi süre boyunca kýstýlama sürer
        if(countDownTime!=0)
        {
            countDownTimer -= Time.deltaTime;

            if(countDownTimer<=0)
            {
                countDownTimer = countDown;
                Restrict(false,false,false,0);      // herþeyi aktif hale getir
                Debug.Log("hersey aktif, kýstlayýcý alaný kaldýrýldý");
                gameObject.SetActive(false);            
            }
        }
       

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
           Restrict(this.shouldRestrictLeft, this.shouldRestrictRight, this.shouldRestrictJump, this.countDown);
        }
    }
    private void Restrict(bool left,bool right, bool jump, float countDown)
    {

        Debug.Log("Restrict. sol: " + left.ToString() + " sag: " + right.ToString() + " zýplama: " + jump.ToString()+" countdown: "+countDown.ToString()) ;
        PlayerController.instance.isLeftRestricted = left;
        PlayerController.instance.isRightRestricted = right;
        PlayerController.instance.isJumpRestricted = jump;
        this.countDownTime = countDown;
        this.countDownTimer = countDownTime;
        StartCoroutine(info());
        
    }
    IEnumerator info()
    {
        StoryTeller.instance.panel.SetActive(true);
        StoryTeller.instance.EditUI("OFY", ("is left movement restricted? =" + shouldRestrictLeft + "\nis right movement restricted? = " + shouldRestrictRight + "\nis jump restricted? = " + shouldRestrictJump));
        yield return new WaitForSeconds(4);
        StoryTeller.instance.panel.SetActive(false);
    }

}
