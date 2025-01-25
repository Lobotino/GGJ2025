using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstDialogLogic : MonoBehaviour
{
    public SmoothMove boss;
    public SmoothMove player;

    public GameObject noText;
    public GameObject bossText;
    public GameObject playerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        noText.SetActive(true);
        boss.StartMoving();
        yield return new WaitForSeconds(2);
        noText.SetActive(false);
        bossText.SetActive(true);
        yield return new WaitForSeconds(6);
        bossText.SetActive(false);
        noText.SetActive(true);

        boss.StartMovingBack();
        yield return new WaitForSeconds(2);
        player.StartMoving();
        yield return new WaitForSeconds(2);
        noText.SetActive(false);
        playerText.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Game");
    }
}