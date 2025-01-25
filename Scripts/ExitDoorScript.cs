using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("Menu");
    }
}
