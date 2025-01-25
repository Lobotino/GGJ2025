using UnityEngine;
using System.Collections;

public class BubbleTapOnColliderController : MonoBehaviour
{
    public AudioSource audioSource;
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

    public FreeModeCounter freeModeCounter;

    void Start()
    {
        freeModeCounter = GameObject.Find("FreeModeCounter").GetComponent<FreeModeCounter>();
        // Получаем SpriteRenderer объекта
        bubblePoppingController = GetComponentInParent<BubblePoppingController>();
        bubbleRightLogic = GetComponentInParent<BubbleRightLogic>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Play();
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

            PlayerPrefs.SetInt("popsCount", PlayerPrefs.GetInt("popsCount", 0) + 1);
            PlayerPrefs.Save();
            freeModeCounter.IncrementCounter();
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