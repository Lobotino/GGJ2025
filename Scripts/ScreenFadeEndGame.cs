using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFadeEndGame : MonoBehaviour
{
    public Image fadeImage;      // Ссылка на Image, который будет затемняться/раззатемняться
    public float fadeDuration = 1f; // Длительность анимации в секундах
    public float timeout = 2f;   // Таймаут перед затемнением
    
    private void Start()
    {
        if (fadeImage != null)
        {
            // Установить начальное состояние — полностью черный экран
            SetAlpha(1);
            StartCoroutine(StartSequence());
        }
    }

    // Запуск последовательности: раззатемнение → таймаут → затемнение
    private IEnumerator StartSequence()
    {
        yield return Fade(1, 0); // Раззатемнение
        yield return new WaitForSeconds(timeout); // Таймаут
        yield return Fade(0, 1); // Затемнение
        SceneManager.LoadScene("Menu");
    }

    // Плавное затемнение/раззатемнение
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(endAlpha); // Гарантируем, что альфа станет конечным значением
    }

    // Метод для установки альфа-канала
    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }
}