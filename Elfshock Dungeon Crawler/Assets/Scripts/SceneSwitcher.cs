using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("InGame");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
