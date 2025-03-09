using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
        //Audio Manager
    }

    public void Click()
    {
        //Audio Manager
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OptionsScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
