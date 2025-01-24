using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleFieldGenerator : MonoBehaviour
{
    public bool isConvayer = false;

    public GameObject bubbleField;

    // Префабы для объектов
    public GameObject bubbleObject;

    public SpriteRenderer instructionSpriteObject;
    public int currentCorrectPopsIndex = 0;
    public Sprite insrtuctionWrongSprite;
    public Sprite insrtuctionRightSprite;
    public Sprite[] instructionSprites;

    private readonly int[][,] _correctPops =
    {
        new[,]
        {
            { 0, 0, 1, 1, 1, 1 },
              { 0, 1, 0, 0, 1, 0 },
            { 0, 0, 1, 0, 0, 1 },
              { 0, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0 },
        },
        new[,]
        {
            { 0, 0, 0, 0, 1, 1 },
                { 0, 0, 1, 1, 1, 0 },
            { 0, 0, 1, 1, 1, 0 },
                { 1, 1, 1, 0, 0, 0 },
            { 1, 1, 0, 0, 0, 0 },
        },
    };

    // Радиус гексагона (расстояние между центрами соседних гексагонов)
    private float hexRadius;

    // Время между обновлениями поля (в секундах)
    public float updateInterval = 3.0f;

    private GameObject currentBubbleField = null;

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
            GenerateField(_correctPops[currentCorrectPopsIndex]);

            // Ждём указанное время
            yield return new WaitForSeconds(updateInterval);
        }
    }

    /// <summary>
    /// Генерирует гексагональное игровое поле на основе двумерного массива.
    /// </summary>
    /// <param name="field">Двумерный массив (0 и 1), определяющий типы объектов.</param>
    public void GenerateField(int[,] field)
    {
        if (!isConvayer)
        {
            Destroy(currentBubbleField);
        }

        currentBubbleField = Instantiate(bubbleField, transform.position + new Vector3(0f, 0f, -1f), Quaternion.identity);

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
                if (isConvayer)
                {
                    prefabToInstantiate.GetComponent<BubbleRightLogic>().isBubbleRight = field[y, x] == 1;
                }
                else
                {
                    if (field[y, x] == 1)
                    {
                        prefabToInstantiate.GetComponent<SpriteRenderer>().sprite = insrtuctionRightSprite;
                    }
                    else
                    {
                        prefabToInstantiate.GetComponent<SpriteRenderer>().sprite = insrtuctionWrongSprite;
                    }
                }

                // Вычисляем позицию для гексагона
                float xPos = x * xOffset;
                if (y % 2 != 0) // Смещаем нечетные строки
                {
                    xPos += xOffset / 2f;
                }

                float yPos = -y * yOffset; // Сдвигаем строки вниз

                // Создаем объект и добавляем его в иерархию
                Vector3 position = currentBubbleField.transform.position + new Vector3(xPos, yPos, 0);
                Instantiate(prefabToInstantiate, position, Quaternion.identity, currentBubbleField.transform);

                if (!isConvayer)
                {
                    instructionSpriteObject.sprite = instructionSprites[currentCorrectPopsIndex];
                }
            }
        }

        if (currentCorrectPopsIndex + 1 >= _correctPops.Length)
        {
            currentCorrectPopsIndex = 0;
        }
        else
        {
            currentCorrectPopsIndex++;
        }
    }

    // Получаем SpriteRenderer дочернего объекта с тегом "background"
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