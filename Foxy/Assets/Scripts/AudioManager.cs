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
        soundEffects[soundToPlay].Stop();   // e�er hali haz�rda �al�yorsa tekrar �almaz onun i�in �nce durdurup sonra �al�cazz

        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);  //ayn� sesi farkl� frekanslarda vericek biraz daha ince ve kal�n
                                                                    //amac� oyuncunun ayn� sesi duymaktan s�k�lmamas� 
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
