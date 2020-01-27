using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isClose = false;

    public Slider slider;

    public AudioMixer audioMixer;

    public void PlayeGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UIEnable()
    {
        GameObject.Find("Canvas/Menu/UI").SetActive(true);
    }

    public void Puase()
    {
        isClose = !isClose;
        pauseMenu.SetActive(isClose);
        PuaseGame();
    }

    public void ClosePuase()
    {
        isClose = false;
        pauseMenu.SetActive(isClose);
        PuaseGame();
    }

    private void PuaseGame()
    {
        if (isClose)
        {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("MainVolume", slider.value);
    }
}
