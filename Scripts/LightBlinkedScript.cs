using System.Collections;
using UnityEngine;

public class LightBlinkedScript : MonoBehaviour
{
    public GameObject darkBackground;
    public GameObject lightBackground;

    public bool needToBlink = true; // Флаг для управления миганием

    private void Start()
    {
        // Запускаем корутину для мигания света
        StartCoroutine(BlinkLight());
    }

    private IEnumerator BlinkLight()
    {
        while (needToBlink)
        {
            float randomInterval = Random.Range(2f, 6f);

            // Включаем lightBackground и выключаем darkBackground
            lightBackground.SetActive(true);
            darkBackground.SetActive(false);

            // Ждем случайный интервал
            yield return new WaitForSeconds(randomInterval);

            // Два быстрых мигания
            for (int i = 0; i < 2; i++)
            {
                lightBackground.SetActive(false);
                darkBackground.SetActive(true);
                yield return new WaitForSeconds(0.15f); // Задержка в 0.1 секунды (быстрое выключение)

                lightBackground.SetActive(true);
                darkBackground.SetActive(false);
                yield return new WaitForSeconds(0.1f); // Задержка в 0.1 секунды (быстрое включение)
            }
        }
    }
}