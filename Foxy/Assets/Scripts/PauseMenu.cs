using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public string levelSelect, mainMenu;
    public GameObject pauseScreen;
    public bool isPaused = false;


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
        if(Input.GetKeyDown(KeyCode.Escape))        //esc basýlýrsa durdur
        {
            PauseUnpause();
        }

    }

    public void PauseUnpause()
    {
        if(isPaused)        // çöz unpause
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;            // zaman akmaya devametsin
        }
        else                //durdur pause 
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;        //zaman dursun pauseda oynanamasýn
        }
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
    public void SelectLevel()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

}

