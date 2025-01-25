using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class InstructionsGenerator : MonoBehaviour
{
    public Sprite insrtuctionWrongSprite;
    public Sprite insrtuctionRightSprite;

    public Sprite[] instructionSpritesItems;
    public SpriteRenderer instructionSpriteObject;

    public float updateInstructionsInterval = 15f;
    public GameObject bubbleField;
    public GameObject bubbleObject;
    public LampState instructionsLampStateUpdater;

    private readonly int[][,] _correctInstructions =
    {
        new[,] //наушники
        {
            { 0, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 1, 0, 0 },
            { 0, 1, 0, 0, 1, 0 },
            { 0, 1, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
        },
        new[,] //зонт
        {
            { 0, 0, 0, 0, 1, 1 },
            { 0, 0, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 0 },
            { 1, 1, 1, 0, 0, 0 },
            { 1, 1, 0, 0, 0, 0 },
        },
        new[,] //кораблик
        {
            { 0, 0, 0, 1, 0, 0 },
            { 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 1, 0, 0 },
            { 1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 1, 0 },
        },

        new[,] //чемодан
        {
            { 0, 0, 1, 1, 0, 0 },
            { 1, 1, 1, 1, 1, 0 },
            { 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0 },
        },

        new[,] //яблочко
        {
            { 0, 0, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 0, 0 },
            { 0, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 0, 0 },
            { 0, 0, 1, 1, 0, 0 },
        },

        new[,] //грибочек
        {
            { 0, 1, 1, 1, 0, 0 },
            { 1, 1, 1, 0, 0, 0 },
            { 0, 1, 1, 1, 1, 0 },
            { 0, 1, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0 },
        },


        new[,] //гитара
        {
            { 0, 0, 0, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 0, 0 },
            { 0, 1, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0 },
        },


        new[,] //черпак
        {
            { 0, 1, 1, 0, 0, 0 },
            { 1, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0 },
        },


        new[,] //ножницы
        {
            { 0, 0, 1, 0, 0, 0 },
            { 0, 1, 1, 0, 0, 0 },
            { 0, 1, 1, 1, 0, 0 },
            { 0, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
        },


        new[,] //ведро
        {
            { 0, 0, 1, 1, 0, 0 },
            { 0, 1, 1, 1, 0, 0 },
            { 0, 1, 1, 1, 1, 0 },
            { 0, 1, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
        },
    };

    private GameObject currentBubbleField = null;
    private float hexRadius;

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
        StartCoroutine(GenerateInstructionsPeriodically());
    }

    private int _currentInstructionsIndex = 0;

    public int[,] GetCurrentInstructions()
    {
        return _correctInstructions[_currentInstructionsIndex];
    }

    private IEnumerator GenerateInstructionsPeriodically()
    {
        while (true)
        {
            // Генерируем поле
            ChangeInstructions();
            ChangeInstructionItem();
            GenerateField(_correctInstructions[_currentInstructionsIndex]);

            // Ждём указанное время
            yield return new WaitForSeconds(updateInstructionsInterval);
        }
    }

    private void ChangeInstructionItem()
    {
        instructionSpriteObject.sprite = instructionSpritesItems[_currentInstructionsIndex];
    }

    private void ChangeInstructions()
    {
        var lastIndex = _currentInstructionsIndex;
        var nextIndex = lastIndex;

        while (nextIndex == lastIndex)
        {
            nextIndex = Random.Range(0, _correctInstructions.Length);
        }

        _currentInstructionsIndex = nextIndex;
        instructionsLampStateUpdater.BlinkYellow();
    }

    /// <summary>
    /// Генерирует гексагональное игровое поле на основе двумерного массива.
    /// </summary>
    /// <param name="field">Двумерный массив (0 и 1), определяющий типы объектов.</param>
    private void GenerateField(int[,] field)
    {
        Destroy(currentBubbleField);

        currentBubbleField =
            Instantiate(bubbleField, transform.position + new Vector3(0f, 0f, -1f), Quaternion.identity);

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

                if (field[y, x] == 1)
                {
                    prefabToInstantiate.GetComponent<SpriteRenderer>().sprite = insrtuctionRightSprite;
                }
                else
                {
                    prefabToInstantiate.GetComponent<SpriteRenderer>().sprite = insrtuctionWrongSprite;
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
            }
        }
    }
}