using System;
using System.Collections;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public bool isConvayerWorks = true;
    public int score = 0;

    public float convayerSpeedModifier = 1f; // Начальное значение модификатора
    public float reduceDuration = 2f; // Время, за которое модификатор уменьшится до 0

    private Coroutine reduceCoroutine;

    public InstructionsGenerator instructionsGenerator;

    public LampState finishLamp;

    public int countSuccessScore = 0;

    public GameObject freeGameExitDoor;

    public void Start()
    {
        freeGameExitDoor.SetActive(FreeGameData.IsFreeGameEnabled);
    }

    public void AddTotalScore(int score)
    {
        this.score += score;
    }

    public void OnFailedPopsFinish()
    {
        if (!FreeGameData.IsFreeGameEnabled)
        {
            finishLamp.BlinkRed();
        }
    }

    public void OnSuccessPopsFinish()
    {
        if (!FreeGameData.IsFreeGameEnabled)
        {
            instructionsGenerator.ChangeInstructions();
            countSuccessScore++;
            if (countSuccessScore > 7)
            {
                GameOverByWin();
            }
            finishLamp.BlinkGreen();
        }
    }

    public void GameOverByWin()
    {
        isConvayerWorks = false;
        StartReducingConvayer();
        StartCoroutine(StartGameWin());
    }

    public void GameOverByBoss()
    {
        isConvayerWorks = false;
        StartReducingConvayer();
        StartCoroutine(StartGameOver());
    }

    private IEnumerator StartGameWin()
    {
        FirebaseAnalytics.LogEvent("game_win");
        yield return new WaitForSeconds(reduceDuration + 1.5f);
        SceneManager.LoadScene("GoodEnding");
    }

    private IEnumerator StartGameOver()
    {
        FirebaseAnalytics.LogEvent("game_over");
        yield return new WaitForSeconds(reduceDuration + 1.5f);
        SceneManager.LoadScene("BadEnding");
    }

    /// <summary>
    /// Запускает уменьшение модификатора до нуля.
    /// </summary>
    public void StartReducingConvayer()
    {
        // Останавливаем текущую корутину, если она уже работает
        if (reduceCoroutine != null)
        {
            return;
        }

        // Запускаем новую корутину
        reduceCoroutine = StartCoroutine(ReduceModifierToZero());
    }

    /// <summary>
    /// Корутине для плавного уменьшения модификатора.
    /// </summary>
    private IEnumerator ReduceModifierToZero()
    {
        float startModifier = convayerSpeedModifier;
        float elapsedTime = 0f;

        while (elapsedTime < reduceDuration)
        {
            elapsedTime += Time.deltaTime;
            convayerSpeedModifier = Mathf.Lerp(startModifier, 0f, elapsedTime / reduceDuration);
            yield return null;
        }

        convayerSpeedModifier = 0f; // Гарантируем, что модификатор стал равен 0
    }
}