using UnityEngine;
using System.Collections;

public class BubbleTapOnColliderController : MonoBehaviour
{
    private bool isTouching = false; // Флаг начала касания
    private float touchStartTime; // Время начала касания
    public Sprite poppedSprite; // Новый спрайт, который нужно установить
    private SpriteRenderer spriteRenderer;

    public bool isPopped = false;

    //частицы
    public ParticleSystem particleSystem; // Ссылка на систему частиц

    void Start()
    {
        // Получаем SpriteRenderer объекта
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        if (spriteRenderer == null)
        {
            Debug.LogError("На объекте отсутствует SpriteRenderer!");
        }

        if (particleSystem == null)
        {
            Debug.LogError("На объекте отсутствует ParticleSystem!");
        }
    }

    void OnMouseDown()
    {
        if (isPopped) return;

        OnTap();
    }

    public void OnTap()
    {
        if (isPopped) return;
        isPopped = true;
        if (spriteRenderer != null && poppedSprite != null)
        {
            // Заменяем спрайт
            spriteRenderer.sprite = poppedSprite;
            particleSystem.Play();
            Vibrate();
        }
        else
        {
            Debug.LogWarning("SpriteRenderer или новый спрайт не назначены.");
        }
    }

    private void Vibrate()
    {
        Handheld.Vibrate();
    }
}