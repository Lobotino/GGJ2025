using UnityEngine;
using UnityEngine.Serialization;

public class GameLogicScript : MonoBehaviour
{
    public Vector3 topPosition; // Верхнее положение камеры
    public Vector3 bottomPosition; // Нижнее положение камеры
    public float smoothSpeed = 2f; // Скорость плавного перемещения камеры

    private bool isInTopPosition = false; // Флаг текущего состояния камеры

    void Start()
    {
        // Проверяем наличие гироскопа
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogError("Гироскоп не поддерживается на этом устройстве.");
        }

        // Устанавливаем начальное положение камеры
        transform.position = bottomPosition;
    }

    void Update()
    {
        if (!SystemInfo.supportsGyroscope)
            return;

        // Получаем текущий угол поворота телефона по оси X
        float tilt = GetPhoneTilt();

        Debug.Log("Tilt: " + tilt);

        // Проверяем, нужно ли переключить состояние камеры
        if (tilt is > 310 or < 40)
        {
            if (isInTopPosition)
            {
                isInTopPosition = false; // Переходим в верхнее состояние
            }
        }
        else if (!isInTopPosition)
        {
            isInTopPosition = true; // Возвращаемся в нижнее состояние
        }

        // Плавно перемещаем камеру в целевое положение
        Vector3 targetPosition = isInTopPosition ? topPosition : bottomPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    private float GetPhoneTilt()
    {
        return Input.gyro.attitude.eulerAngles.y;
    }
}