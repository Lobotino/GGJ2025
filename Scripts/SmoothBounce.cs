using UnityEngine;

public class SmoothBounce : MonoBehaviour
{
    public float bounceHeight = 2f;  // Максимальная высота прыжка
    public float bounceSpeed = 2f;   // Скорость прыжка

    private Vector3 startPosition;

    void Start()
    {
        // Запоминаем начальную позицию объекта
        startPosition = transform.position;
    }

    void Update()
    {
        // Рассчитываем новое положение по оси Y
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}