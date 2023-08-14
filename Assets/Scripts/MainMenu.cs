using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float volume;
    //public int respawnPoint;
    //public int harnessNumber;

    public Image image;
    public Image logo;
    public GameObject mainMenu;

    public void Update()
    {
        AudioListener.volume = FindObjectOfType<MainMenu>().volume;
        Debug.Log(AudioListener.volume);
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        image.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        mainMenu.SetActive(false);
    }

    // public void LoadGame()
    // {
    //     SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    //     image.gameObject.SetActive(false);
    //     mainMenu.SetActive(false);
    // }

    public void QuitGame()
    {
        if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
    }

    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
    }
}
