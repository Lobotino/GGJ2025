using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToNormalGame()
    {
        FreeGameData.IsFreeGameEnabled = false;
        LoadGame();
    }

    public void GoToFreeGame()
    {
        FreeGameData.IsFreeGameEnabled = true;
        LoadGame();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}