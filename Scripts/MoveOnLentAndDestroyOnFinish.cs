using UnityEngine;

public class MoveOnLentAndDestroyOnFinish : MonoBehaviour
{
    public float speed = 3f;

    // Позиция, где объект исчезает (справа)
    public float endX = 15f;

    public BubblePoppingController bubblePoppingController;
    private GameController _gameController;

    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        bubblePoppingController = GetComponent<BubblePoppingController>();
    }

    void FixedUpdate()
    {
        transform.position += Vector3.right * speed * Time.deltaTime * _gameController.convayerSpeedModifier;

        if (transform.position.x > endX)
        {
            bubblePoppingController.OnBubbleFinishLent();
            Destroy(gameObject);
        }
    }
}