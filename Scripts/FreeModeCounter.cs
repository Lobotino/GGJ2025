using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeModeCounter : MonoBehaviour
{
    private int counter = 0;

    public Sprite[] numbers;

    public SpriteRenderer spriteRendererFirstNumber;
    public SpriteRenderer spriteRendererSecondNumber;
    public SpriteRenderer spriteRendererThirdNumber;

    public void IncrementCounter()
    {
        if (!FreeGameData.IsFreeGameEnabled)
        {
            return;
        }

        if (counter >= 999)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }

        UpdateSprites();
    }

    private void UpdateSprites()
    {
        spriteRendererFirstNumber.sprite = numbers[counter / 100];
        spriteRendererSecondNumber.sprite = numbers[counter / 10 % 10];
        spriteRendererThirdNumber.sprite = numbers[counter % 10];
    }
}