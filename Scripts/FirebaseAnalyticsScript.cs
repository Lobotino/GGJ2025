using Firebase;
using Firebase.Analytics;
using UnityEngine;

public class FirebaseAnalyticsScript : MonoBehaviour
{
    void Start()
    {
        // Проверяем инициализацию Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Firebase успешно инициализирован!");
                // Здесь можно отправлять события
                LogCustomEvent();
            }
            else
            {
                Debug.LogError($"Не удалось инициализировать Firebase: {dependencyStatus}");
            }
        });
    }

    // Пример логирования события
    void LogCustomEvent()
    {
        // Простое событие
        FirebaseAnalytics.LogEvent("game_start");

        Debug.Log("Событие отправлено!");
    }
}