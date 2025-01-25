using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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