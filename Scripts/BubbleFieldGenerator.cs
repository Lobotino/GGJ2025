using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleFieldGenerator : MonoBehaviour
{
    public GameObject bubbleField;

    // Префабы для объектов
    public GameObject bubbleObject;

    // Радиус гексагона (расстояние между центрами соседних гексагонов)
    private float hexRadius;

    // Время между обновлениями поля (в секундах)
    public float updateInterval = 3.0f;

    // Поле данных
    private int[,] currentField;

    private void Start()
    {
        // Вычисляем размер ячейки на основе размера первого объекта
        if (bubbleObject != null)
        {
            SpriteRenderer spriteRenderer = bubbleObject.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                hexRadius = spriteRenderer.bounds.size.x / 2f; // Половина ширины спрайта
            }
        }

        // Пример начального поля
        currentField = new int[,]
        {
            { 0, 1, 1, 0, 1, 0 },
            { 0, 1, 1, 1, 1, 0 },
            { 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0 }
        };

        // Запуск корутины
        StartCoroutine(GenerateFieldPeriodically());
    }

    /// <summary>
    /// Корус для периодического обновления поля.
    /// </summary>
    private IEnumerator GenerateFieldPeriodically()
    {
        while (true)
        {
            // Генерируем поле
            GenerateField(currentField);

            // Ждём указанное время
            yield return new WaitForSeconds(updateInterval);
        }
    }

    // /// <summary>
    // /// Генерирует игровое поле на основе двумерного массива.
    // /// </summary>
    // /// <param name="field">Двумерный массив (0 и 1), определяющий типы объектов.</param>
    // private void GenerateField(int[,] field)
    // {
    //     var bubbleParent = Instantiate(bubbleField, transform.position, Quaternion.identity);
    //
    //     int rows = field.GetLength(0);
    //     int cols = field.GetLength(1);
    //
    //     for (int y = 0; y < rows; y++)
    //     {
    //         for (int x = 0; x < cols; x++)
    //         {
    //             // Определяем, какой префаб использовать
    //             GameObject prefabToInstantiate = field[y, x] == 1 ? rightBubble : wrongBubble;
    //
    //             // Вычисляем позицию для объекта
    //             Vector3 position = bubbleParent.transform.position + new Vector3(x * cellSize, -y * cellSize, -3);
    //
    //             // Создаем объект и добавляем его в иерархию
    //             Instantiate(prefabToInstantiate, position, Quaternion.identity, bubbleParent.transform);
    //         }
    //     }
    // }

    /// <summary>
    /// Генерирует гексагональное игровое поле на основе двумерного массива.
    /// </summary>
    /// <param name="field">Двумерный массив (0 и 1), определяющий типы объектов.</param>
    public void GenerateField(int[,] field)
    {
        var bubbleParent = Instantiate(bubbleField, transform.position, Quaternion.identity);

        int rows = field.GetLength(0);
        int cols = field.GetLength(1);

        float xOffset = 2f * hexRadius; // Горизонтальное смещение между гексагонами
        float yOffset = 1.5f * hexRadius; // Вертикальное смещение между строками

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                // Определяем, какой префаб использовать
                GameObject prefabToInstantiate = bubbleObject;
                prefabToInstantiate.GetComponent<BubbleRightLogic>().isBubbleRight = field[y, x] == 1;

                // Вычисляем позицию для гексагона
                float xPos = x * xOffset;
                if (y % 2 != 0) // Смещаем нечетные строки
                {
                    xPos += xOffset / 2f;
                }

                float yPos = -y * yOffset; // Сдвигаем строки вниз

                // Создаем объект и добавляем его в иерархию
                Vector3 position = bubbleParent.transform.position + new Vector3(xPos, yPos, 0);
                // Создаем объект и добавляем его в иерархию
                Instantiate(prefabToInstantiate, position, Quaternion.identity, bubbleParent.transform);
            }
        }
    }
}