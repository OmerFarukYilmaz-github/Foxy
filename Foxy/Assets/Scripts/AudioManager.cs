using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource backgroundMusic, levelEndMusic;

    public AudioSource bossMusic;
    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevelVictory()
    {
        backgroundMusic.Stop();
        levelEndMusic.Play();

    }


    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();   // eðer hali hazýrda çalýyorsa tekrar çalmaz onun için önce durdurup sonra çalýcazz

        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);  //ayný sesi farklý frekanslarda vericek biraz daha ince ve kalýn
                                                                    //amacý oyuncunun ayný sesi duymaktan sýkýlmamasý 
        soundEffects[soundToPlay].Play();


    }

    public void PlayBossMusic()
    {
        backgroundMusic.Stop();
        bossMusic.Play();

    }
    public void StopBossMusic()
    {
        bossMusic.Stop();
        backgroundMusic.Play();

    }

}
