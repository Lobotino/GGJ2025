using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    [SerializeField] private Vector2 startPoint; // Стартовая позиция объекта
    [SerializeField] private Vector2 endPoint; // Конечная позиция объекта
    [SerializeField] private float moveDuration = 1.5f; // Время на перемещение в секундах

    private float elapsedTime = 0f;
    private bool isMoving = false;


    public void StartMoving()
    {
        isMoving = true;
    }

    public void StartMovingBack()
    {
        (startPoint, endPoint) = (endPoint, startPoint);
        elapsedTime = 0f;
        isMoving = true;
    }

    void Start()
    {
        // Устанавливаем объект в стартовую позицию
        transform.position = startPoint;
    }

    void Update()
    {
        if (isMoving)
        {
            // Увеличиваем прошедшее время
            elapsedTime += Time.deltaTime;

            // Рассчитываем текущую позицию объекта
            float t = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPoint, endPoint, t);

            // Останавливаем перемещение, если достигли конечной точки
            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }
}