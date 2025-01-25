using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BlinkedBoss : MonoBehaviour
{
    public float minBossInterval = 5f;
    public float maxBossInterval = 15f;

    [FormerlySerializedAs("minBossDuration")]
    public float minBossMessageDuration = 2f;

    [FormerlySerializedAs("maxBossDuration")]
    public float maxBossMessageDuration = 5f;

    public GameObject boss;
    public GameObject bossMessage;

    public bool needToBlink = true; // Флаг для управления миганием
    public bool bossIsActive = false;
    public bool messageIsActive = false;

    private GameController gameController;
    private int strikePops = 0;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        // Запускаем корутину для мигания света

        if (!FreeGameData.IsFreeGameEnabled)
        {
            StartCoroutine(BlinkBoss());
        }
    }

    public void OnPoppedOnBossEyes()
    {
        if (!gameController.isConvayerWorks) return;

        Debug.Log("OnPoppedOnBossEyes " + strikePops);
        if (messageIsActive)
        {
            strikePops++;
            if (strikePops > 8)
            {
                GameOverByBoss();
                return;
            }
        }

        if (messageIsActive) return;
        StartCoroutine(ShowMessageWithDelay());
    }

    private void GameOverByBoss()
    {
        gameController.GameOverByBoss();
        needToBlink = false;
        messageIsActive = false;
        bossIsActive = false;
        boss.SetActive(true);
        bossMessage.SetActive(false);
    }

    private IEnumerator ShowMessageWithDelay()
    {
        bossMessage.SetActive(true);
        messageIsActive = true;
        yield return new WaitForSeconds(Random.Range(minBossMessageDuration, maxBossMessageDuration));
        bossMessage.SetActive(false);
        messageIsActive = false;
    }

    private IEnumerator BlinkBoss()
    {
        while (needToBlink)
        {
            float randomBossInterval = Random.Range(minBossInterval, maxBossInterval);
            // Ждем случайный интервал
            yield return new WaitForSeconds(randomBossInterval);

            bossIsActive = !bossIsActive;
            if (!bossIsActive)
            {
                strikePops = 0;
            }

            boss.SetActive(bossIsActive);
        }
    }
}