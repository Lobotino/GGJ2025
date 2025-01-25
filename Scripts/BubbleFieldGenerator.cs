using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleFieldGenerator : MonoBehaviour
{
    public InstructionsGenerator instructionsGenerator;

    public LampState lampStateUpdater;

    public GameObject bubbleField;

    // Префабы для объектов
    public GameObject bubbleObject;

    // Радиус гексагона (расстояние между центрами соседних гексагонов)
    private float hexRadius;

    // Время между обновлениями поля (в секундах)
    public float createBubblesInterval = 4.0f;

    private GameObject currentBubbleField = null;

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
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
        while (gameController.isConvayerWorks)
        {
            // Генерируем поле
            GenerateField(instructionsGenerator.GetCurrentInstructions());

            // Ждём указанное время
            yield return new WaitForSeconds(FreeGameData.IsFreeGameEnabled ? 2.5f : createBubblesInterval);
        }
    }

    /// <summary>
    /// Генерирует гексагональное игровое поле на основе двумерного массива.
    /// </summary>
    /// <param name="field">Двумерный массив (0 и 1), определяющий типы объектов.</param>
    private void GenerateField(int[,] field)
    {
        currentBubbleField =
            Instantiate(bubbleField, transform.position + new Vector3(0f, 0f, -1f), Quaternion.identity);

        int rows = field.GetLength(0);
        int cols = field.GetLength(1);

        float xOffset = 2f * hexRadius; // Горизонтальное смещение между гексагонами
        float yOffset = 1.5f * hexRadius; // Вертикальное смещение между строками

        var rightBubblesCount = 0;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                // Определяем, какой префаб использовать
                GameObject prefabToInstantiate = bubbleObject;
                prefabToInstantiate.GetComponent<BubbleRightLogic>().isBubbleRight = field[y, x] == 1;

                if (field[y, x] == 1) rightBubblesCount++;

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
            }
        }

        currentBubbleField.GetComponent<BubblePoppingController>().SetRightPopsCount(rightBubblesCount);
        currentBubbleField.GetComponent<BubblePoppingController>().SetPopSchemeIndex(instructionsGenerator.GetCurrentInstructionsIndex());
    }
}