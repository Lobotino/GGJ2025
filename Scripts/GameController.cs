using System;
using System.Collections;
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

    public ArrayList alreadyCompletedFormsIndexes = new ArrayList();

    public LampState finishLamp;

    private void TryAddCompletedFormsIndex(int index)
    {
        if (!alreadyCompletedFormsIndexes.Contains(index))
        {
            alreadyCompletedFormsIndexes.Add(index);
            if (alreadyCompletedFormsIndexes.Count >= 5)
            {
                GameOverByWin();
            }
        }
    }

    public void AddTotalScore(int score)
    {
        this.score += score;
    }

    public void OnFailedPopsFinish()
    {
        if (FreeGameData.IsFreeGameEnabled)
        {
            finishLamp.BlinkGreen();
        }
        else
        {
            finishLamp.BlinkRed();
        }
    }

    public void OnSuccessPopsFinish(int currentInstructionsIndex)
    {
        if (!FreeGameData.IsFreeGameEnabled)
        {
            TryAddCompletedFormsIndex(currentInstructionsIndex);
        }

        finishLamp.BlinkGreen();
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
        yield return new WaitForSeconds(reduceDuration + 1.5f);
        SceneManager.LoadScene("GoodEnding");
    }

    private IEnumerator StartGameOver()
    {
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