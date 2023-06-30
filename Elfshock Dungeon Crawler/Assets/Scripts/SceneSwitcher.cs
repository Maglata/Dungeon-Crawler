using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    public void startGame()
    {
        SceneManager.LoadScene("InGame");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    private void Awake()
    {
        if(PointSystem.LoadPoints() != null)
        {
            coinText.text = $"Coins: {PointSystem.LoadPoints().points}";
        }
    }
}
