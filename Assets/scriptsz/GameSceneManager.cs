using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();

        // This will stop play mode in the editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void LoadMainMenu()
    {
        // Replace "MainMenu" with the name of your main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
