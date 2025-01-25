using System.Collections;
using UnityEngine;

public class MenuBlinkedScript : MonoBehaviour
{
    public GameObject darkBackground;
    public GameObject lightBackground;

    public bool needToBlink = true; // Флаг для управления миганием

    public float interval = 1.5f;
    private bool _isActive = true;

    public void Start()
    {
        // Запускаем корутину для мигания света
        StartCoroutine(BlinkLight());
    }

    private IEnumerator BlinkLight()
    {
        while (needToBlink)
        {
            // Включаем lightBackground и выключаем darkBackground
            lightBackground.SetActive(_isActive);
            darkBackground.SetActive(!_isActive);

            _isActive = !_isActive;
            // Ждем случайный интервал
            yield return new WaitForSeconds(interval);
        }
    }
}