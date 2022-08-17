using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitbox : MonoBehaviour
{
    public BossTankController bossCtrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //hitboxa ustten deðince
        if(other.tag=="Player"  && PlayerController.instance.transform.position.y > transform.position.y)
        {
            bossCtrl.TakeHit();

            PlayerController.instance.Bounce();

            gameObject.SetActive(false);        // vurunca animasyona girip tanka saklanacak bu sýrada vuramayalým
        }
    }
}
