using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToRespawn;

    public int gemsCollected=0;
    public string levelToLoad;
    public float timeInLevel;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Oyunda ge�en s�re
        timeInLevel += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R))
        {
            string levelName = SceneManager.GetActiveScene().name;

            new WaitForSeconds(waitToRespawn +15f );         // biraz bekle
            UIController.instance.FadeToBlack();    // ekran� karart

            SceneManager.LoadScene(levelName);
        }

    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn Player");
        StartCoroutine(RespawnCo());
   
    }

    // unityden ayr�l�p kendi zaman�n� a�ar, update gibi loopta olmaz. kendi zaman�n� a�ar, bekler i�ini bitirir sonlan�r. 
    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);  //s�n�f�n ba�l� oldu�u nesneyi deaktif et
        AudioManager.instance.PlaySFX(8);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));         // biraz bekle
        UIController.instance.FadeToBlack();    // ekran� karart

        yield return new WaitForSeconds( (1f / UIController.instance.fadeSpeed) + .2f);         // biraz bekle ekran tam siyah kals�n
        UIController.instance.FadeFromBlack();    // ekran� a� 

        FallingPlatformController.instance.RespawnFallingPlatforms();       // levelde tekrar respawn oldugumuzda  dusen platformlarda respawn olsun
        try
        {
            RobotRespawnController.instance.RespawnDestroyedRobots();
        }catch
        {
            Debug.Log("levelde robot yok yada hata oldu robotlar spawn olmad�");
        }
     
        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.anim.SetBool("isHurt", false);    //do�duktan sonra animasyonu devam ediyodu etmesin diye
        // son aktifle�tirdi�imiz Checkpointin konumunu al�p oyuncuyu oraya g�nderiyoruz
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        // yeniden do�duktan sonra can� full leyip u� � duzenliyoruz
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {

        string levelName = SceneManager.GetActiveScene().name;

        AudioManager.instance.PlayLevelVictory();
        PlayerController.instance.stopInput = true;

        CameraController.instance.shouldFollowTarget = false;

        UIController.instance.levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) * 2.5f);

        //SceneManager.GetActiveScene().name ile Bulundu�umuz levelin ad�n� al�yoruz
        // + "_unlocked" ile sonuna _unlocked kelimesini ekliyoruz. mesela Level1-1 i bitirdik 
        // PlayerPrefs'e "Level1-1_unlocked" isimli veri tan�mlay�p de�erini 1 yap�yoruz. (1=true gibi d���n, bool eklenemiyor)
        // dict gibi bi�ey
        PlayerPrefs.SetInt(levelName + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", levelName);


        //En �ok toplanm�� gem i kaydet
        if (PlayerPrefs.HasKey(levelName + "_gems"))
        {
            if (gemsCollected > PlayerPrefs.GetInt(levelName + "_gems"))
            { 
                PlayerPrefs.SetInt(levelName + "_gems", gemsCollected);
            } 
        }
        else 
        { 
            PlayerPrefs.SetInt(levelName + "_gems", gemsCollected);
        }

        //En k�sa s�rm�� zaman� kaydet
        if (PlayerPrefs.HasKey(levelName + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(levelName + "_time", timeInLevel))
            {
                PlayerPrefs.SetFloat(levelName + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(levelName + "_time", timeInLevel);
        }

        
        SceneManager.LoadScene(levelToLoad);


    }


}
