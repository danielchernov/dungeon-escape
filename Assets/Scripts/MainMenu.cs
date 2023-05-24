using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] audiosSFX;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        AudioManager.Instance.PlaySFX(audiosSFX[0], 0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
        AudioManager.Instance.PlaySFX(audiosSFX[0], 0.5f);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlaySFX(audiosSFX[0], 0.5f);
    }

    public void PlaySFX()
    {
        AudioManager.Instance.PlaySFX(audiosSFX[1], 0.5f);
    }
}
