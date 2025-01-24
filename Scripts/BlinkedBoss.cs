using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BlinkedBoss : MonoBehaviour
{
    public GameObject boss;

    public bool needToBlink = true; // Флаг для управления миганием
    private bool isActive = true;

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

            boss.SetActive(isActive);
            isActive = !isActive;

            // Ждем случайный интервал
            yield return new WaitForSeconds(randomInterval);
        }
    }
}