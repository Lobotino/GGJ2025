using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSceneListener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем первое касание
        {
            SceneManager.LoadScene("Game");
        }
    }
}