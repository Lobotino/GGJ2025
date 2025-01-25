using System;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    public GameController gameController;
    public Sprite[] numbers;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int currentNumber = 0;

    // Update is called once per frame
    void Update()
    {
        if (currentNumber != gameController.countSuccessScore)
        {
            currentNumber = gameController.countSuccessScore;   
            spriteRenderer.sprite = numbers[currentNumber];
        }
    }
}