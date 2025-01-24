using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    // Скорость движения объекта
    public float speed = 5f;

    // Позиция, где объект появляется (слева)
    public float startX = -10f;

    // Позиция, где объект исчезает (справа)
    public float endX = 10f;

    void FixedUpdate()
    {
        // Двигаем объект влево
        transform.position += Vector3.right * speed * Time.deltaTime;

        // Если объект пересекает правую границу экрана
        if (transform.position.x > endX)
        {
            // Пересоздаем объект слева
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        // Устанавливаем позицию объекта на старте
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }
}