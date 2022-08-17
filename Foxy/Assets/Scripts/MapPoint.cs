using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel, isLocked;
    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;

    // Start is called before the first frame update
    void Start()
    {
        if(isLevel && levelToLoad != null)
        {

            if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }

            if(PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }

            if(gemsCollected >= totalGems)
            {
                gemBadge.SetActive(true);
            }

            if(bestTime <= targetTime && bestTime != 0)
            {
                timeBadge.SetActive(true);
            }

            isLocked = true;

            if(levelToCheck != null)        //her level için kontrol edilecek önceki bir level var
            {

                //PlayerPrefs.HasKey() boyle bir değer var mı diye bakıyor
                //levelToCheck olarak vereceğimiz level adı + _unlocked isimli bir veri var mı
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    // varsa değeri 1 mi yani true mu açılmış mı
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                } 
            }


            // ilk level için levelToLoad ve levelToCheck aynı
            // ilk level kilitli başlayacak
            // oynayabilmek için
            if (levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
