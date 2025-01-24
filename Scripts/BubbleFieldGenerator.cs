using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleFieldGenerator : MonoBehaviour
{
    [SerializeField] private Sprite backgroundCornerTopLeftSprite; // Левый верхний угол
    [SerializeField] private Sprite backgroundCornerTopRightSprite; // Правый верхний угол
    [SerializeField] private Sprite backgroundCornerBottomLeftSprite; // Левый нижний угол
    [SerializeField] private Sprite backgroundCornerBottomRightSprite; // Правый нижний угол
    [SerializeField] private Sprite backgroundEdgeTopSprite; // Верхний край
    [SerializeField] private Sprite backgroundEdgeBottomSprite; // Нижний край
    [SerializeField] private Sprite backgroundEdgeLeftSprite; // Левый край
    [SerializeField] private Sprite backgroundEdgeRightSprite; // Правый край
    [SerializeField] private Sprite backgroundCenterSprite; // Центральный элемент

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
                GameObject bubble = Instantiate(prefabToInstantiate, position, Quaternion.identity,
                    bubbleParent.transform);

                // // Выбираем спрайт для объекта
                // SpriteRenderer spriteRenderer = GetChildSpriteRendererWithTag(prefabToInstantiate, "background");
                // if (spriteRenderer != null)
                // {
                //     spriteRenderer.sprite = GetSpriteForPosition(x, y, rows, cols);
                // }
            }
        }
    }
    //
    // private Sprite GetSpriteForPosition(int x, int y, int rows, int cols)
    // {
    //     // Углы
    //     if (x == 0 && y == 0) // Левый верхний угол
    //         return backgroundCornerTopLeftSprite;
    //     if (x == cols - 1 && y == 0) // Правый верхний угол
    //         return backgroundCornerTopRightSprite;
    //     if (x == 0 && y == rows - 1) // Левый нижний угол
    //         return backgroundCornerBottomLeftSprite;
    //     if (x == cols - 1 && y == rows - 1) // Правый нижний угол
    //         return backgroundCornerBottomRightSprite;
    //
    //     // Верхний край (исключая углы)
    //     if (y == 0 && x > 0 && x < cols - 1)
    //         return backgroundEdgeTopSprite;
    //
    //     // Нижний край (исключая углы)
    //     if (y == rows - 1 && x > 0 && x < cols - 1)
    //         return backgroundEdgeBottomSprite;
    //
    //     // Левый край (с учётом сдвига)
    //     if (x == 0 && y > 0 && y < rows - 1)
    //         return backgroundEdgeLeftSprite;
    //
    //     // Правый край (с учётом сдвига)
    //     if (x == cols - 1 && y > 0 && y < rows - 1)
    //         return backgroundEdgeRightSprite;
    //
    //     // Центральные элементы (все элементы, не попавшие в крайние или угловые случаи)
    //     return backgroundCenterSprite;
    // }
    //
    // // Получаем SpriteRenderer дочернего объекта с тегом "background"
    // SpriteRenderer GetChildSpriteRendererWithTag(GameObject parent, string tag)
    // {
    //     // Находим всех дочерних объектов
    //     Transform[] children = parent.GetComponentsInChildren<Transform>();
    //     foreach (Transform child in children)
    //     {
    //         // Проверяем тег и наличие SpriteRenderer
    //         if (child.CompareTag(tag))
    //         {
    //             return child.GetComponent<SpriteRenderer>();
    //         }
    //     }
    //
    //     // Если ничего не найдено, возвращаем null
    //     return null;
    // }
}