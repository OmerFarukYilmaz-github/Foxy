using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           //unutma

public class UIController : MonoBehaviour
{
    public static UIController instance;        //heryerden eri�ilebilsin diye

    public Image hearth1, hearth2, hearth3;

    public Sprite hearthFull, hearthHalf, hearthEmpty;
    public Text txtGem;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelCompleteText;

    public void Awake()
    {
        instance = this;            // insatance � bu s�n�f�n� temsilcisii yapt�k
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();
        FadeFromBlack();        //ba�lang��ta siyah ekran var o a��ls�n diye
    }



    // Update is called once per frame
    void Update()
    {
          if(shouldFadeToBlack)
          {
            // Mathf.MoveTowards belirli bir �eyi belli bir noktaya verilen artt�rma orna�yla g�t�r�r/artt�r�
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)); //deltaTime kulland�k ki farkl�
                                                                                                                 //fpslerde farkl� sonu�lar almayal�m
            if(fadeScreen.color.a == 1f)        //tamamen siyah oldu�unda art�k fade i bitir
            {
                shouldFadeToBlack = false;
            }
        
          }
        if (shouldFadeFromBlack)
        {
            // Mathf.MoveTowards belirli bir �eyi belli bir noktaya verilen artt�rma orna�yla g�t�r�r/artt�r�
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                                         Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime)); //deltaTime kulland�k ki farkl�
                                                                                                                 //fpslerde farkl� sonu�lar almayal�m
            if (fadeScreen.color.a == 1f)        //tamamen siyah oldu�unda art�k fade i bitir
            {
                shouldFadeFromBlack = false;
            }

        }

    }

    public void UpdateHealthDisplay()
    {
        switch(PlayerHealthController.instance.currentHealth)
        {
            case 6:
                SetImages(hearthFull, hearthFull, hearthFull);
                break;
            case 5:
                SetImages(hearthFull, hearthFull, hearthHalf);
                break;
            case 4:
                SetImages(hearthFull, hearthFull, hearthEmpty);
                break;
            case 3:
                SetImages(hearthFull, hearthHalf, hearthEmpty);
                break;
            case 2:
                SetImages(hearthFull, hearthEmpty, hearthEmpty);
                break;
            case 1:
                SetImages(hearthHalf, hearthEmpty, hearthEmpty);
                break;
            case 0:
                SetImages(hearthEmpty, hearthEmpty, hearthEmpty);
                break;
            default:
                SetImages(hearthEmpty, hearthEmpty, hearthEmpty);
                break;

        }
        
    }

    private void SetImages(Sprite s1, Sprite s2, Sprite s3)
    {
        hearth1.sprite = s1;
        hearth2.sprite = s2;
        hearth3.sprite = s3;
    }
    
    public void UpdateGemCount()
    {
        txtGem.text = LevelManager.instance.gemsCollected.ToString();
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

}
