using System;
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
        FreeGameData.IsFreeGameEnabled = false;
        SceneManager.LoadScene("FirstDialog");
    }

    public void GoToFreeGame()
    {
        FreeGameData.IsFreeGameEnabled = true;
        SceneManager.LoadScene("Game");
    }
}