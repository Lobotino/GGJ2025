using UnityEngine;
using System.Collections;

public class BubbleTapOnColliderController : MonoBehaviour
{
    private bool isTouching = false; // Флаг начала касания
    private float touchStartTime; // Время начала касания
    public Sprite poppedSprite; // Новый спрайт, который нужно установить
    private SpriteRenderer spriteRenderer;
    private BlinkedBoss _blinkedBoss;

    public BubbleRightLogic bubbleRightLogic;

    public bool isPopped = false;

    public BubblePoppingController bubblePoppingController;

    //частицы
    public ParticleSystem particleSystem; // Ссылка на систему частиц

    void Start()
    {
        // Получаем SpriteRenderer объекта
        bubblePoppingController = GetComponentInParent<BubblePoppingController>();
        bubbleRightLogic = GetComponentInParent<BubbleRightLogic>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        _blinkedBoss = GameObject.Find("BlinkedBoss").GetComponent<BlinkedBoss>();
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
            if (_blinkedBoss.bossIsActive)
            {
                _blinkedBoss.OnPoppedOnBossEyes();
            }
            
            // Заменяем спрайт
            spriteRenderer.sprite = poppedSprite;
            particleSystem.Play();
            Vibrate();
            if (bubbleRightLogic.isBubbleRight)
            {
                bubblePoppingController.OnPoppedRight();
            }
            else
            {
                bubblePoppingController.OnPoppedWrong();
            }
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