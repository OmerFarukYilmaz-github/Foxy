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
        //Oyunda geçen süre
        timeInLevel += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R))
        {
            string levelName = SceneManager.GetActiveScene().name;

            new WaitForSeconds(waitToRespawn +15f );         // biraz bekle
            UIController.instance.FadeToBlack();    // ekraný karart

            SceneManager.LoadScene(levelName);
        }

    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn Player");
        StartCoroutine(RespawnCo());
   
    }

    // unityden ayrýlýp kendi zamanýný açar, update gibi loopta olmaz. kendi zamanýný açar, bekler iþini bitirir sonlanýr. 
    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);  //sýnýfýn baðlý olduðu nesneyi deaktif et
        AudioManager.instance.PlaySFX(8);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));         // biraz bekle
        UIController.instance.FadeToBlack();    // ekraný karart

        yield return new WaitForSeconds( (1f / UIController.instance.fadeSpeed) + .2f);         // biraz bekle ekran tam siyah kalsýn
        UIController.instance.FadeFromBlack();    // ekraný aç 

        FallingPlatformController.instance.RespawnFallingPlatforms();       // levelde tekrar respawn oldugumuzda  dusen platformlarda respawn olsun
        try
        {
            RobotRespawnController.instance.RespawnDestroyedRobots();
        }catch
        {
            Debug.Log("levelde robot yok yada hata oldu robotlar spawn olmadý");
        }
     
        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.anim.SetBool("isHurt", false);    //doðduktan sonra animasyonu devam ediyodu etmesin diye
        // son aktifleþtirdiðimiz Checkpointin konumunu alýp oyuncuyu oraya gönderiyoruz
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;

        // yeniden doðduktan sonra caný full leyip uý ý duzenliyoruz
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

        //SceneManager.GetActiveScene().name ile Bulunduðumuz levelin adýný alýyoruz
        // + "_unlocked" ile sonuna _unlocked kelimesini ekliyoruz. mesela Level1-1 i bitirdik 
        // PlayerPrefs'e "Level1-1_unlocked" isimli veri tanýmlayýp deðerini 1 yapýyoruz. (1=true gibi düþün, bool eklenemiyor)
        // dict gibi biþey
        PlayerPrefs.SetInt(levelName + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", levelName);


        //En çok toplanmýþ gem i kaydet
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

        //En kýsa sürmüþ zamaný kaydet
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
