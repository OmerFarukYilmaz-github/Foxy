using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;          //unutma


public class MainMenu : MonoBehaviour
{

    public string startScene,continueScene;
    public GameObject continueButton;
    public GameObject creditPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Daha �nce oynanm�� m�
        if(PlayerPrefs.HasKey(startScene+"_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartGame()
    {

        SceneManager.LoadScene(startScene);

        // kay�tl� t�m verileri siler
        PlayerPrefs.DeleteAll();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void QuitGame()
    {
        Application.Quit();         // unity editorde �al��maz build edilmesi laz�m
        Debug.Log("Exit");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("mainMenu");
        Time.timeScale = 1f;
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
    }
    public void CloseCredit()
    {
        creditPanel.SetActive(false);
    }

}
