using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour
{
    public AudioSource audio;

    public bool isFreeGameButton;

    public MainMenu mainMenu;

    void OnMouseDown()
    {
        audio.Play();
        if (isFreeGameButton)
        {
            mainMenu.GoToFreeGame();
        }
        else
        {
            mainMenu.GoToNormalGame();
        }
    }
}