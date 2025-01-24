using System;
using UnityEngine;
using System.Collections;

public class TapOnCollider : MonoBehaviour
{
    private bool isTouching = false; // Флаг начала касания
    private float touchStartTime; // Время начала касания
    private const float maxTouchDuration = 0.05f; // Максимальная длительность, чтобы считать это "нажатием"
    public Sprite poppedSprite; // Новый спрайт, который нужно установить
    private SpriteRenderer spriteRenderer;

    public bool isPopped = false;

    void Start()
    {
        // Получаем SpriteRenderer объекта
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("На объекте отсутствует SpriteRenderer!");
        }
    }

    void OnMouseDown()
    {
        if (isPopped) return;

        OnTap();
    }

    void OnMouseUp()
    {
        // if (isPopped) return;
        // if (isTouching)
        // {
        //     float touchDuration = Time.time - touchStartTime;
        //     
        //     OnTap();
        //
        //     isTouching = false;
        // }
    }

    private void OnTap()
    {
        isPopped = true;
        if (spriteRenderer != null && poppedSprite != null)
        {
            // Заменяем спрайт
            spriteRenderer.sprite = poppedSprite;
            Vibrate();
            Debug.Log($"Sprite changed on {gameObject.name}");
        }
        else
        {
            Debug.LogWarning("SpriteRenderer или новый спрайт не назначены.");
        }
    }

    private void Vibrate()
    {
        try
        {
            Handheld.Vibrate();
        }
        catch (Exception ex)
        {
            //ignore
        }
    }
}