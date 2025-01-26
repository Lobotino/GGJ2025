using System;
using Firebase.Analytics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI popsCountText;

    private void Start()
    {
        int popsCount = PlayerPrefs.GetInt("popsCount");
        if (popsCount > 0)
        {
            popsCountText.text = "Лопнуто пузырей:" + popsCount;
        }
        else
        {
            popsCountText.gameObject.SetActive(false);
        }
    }

    public void GoToNormalGame()
    {
        FirebaseAnalytics.LogEvent("start_normal_game");
        FreeGameData.IsFreeGameEnabled = false;
        SceneManager.LoadScene("FirstDialog");
    }

    public void GoToFreeGame()
    {
        FirebaseAnalytics.LogEvent("start_free_game");
        FreeGameData.IsFreeGameEnabled = true;
        SceneManager.LoadScene("Game");
    }
}