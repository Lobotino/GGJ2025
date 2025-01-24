using UnityEngine;

public class MoveOnLentAndDestroyOnFinish : MonoBehaviour
{
    public float speed = 3f;
    
    // Позиция, где объект исчезает (справа)
    public float endX = 15f;

    public BubblePoppingController bubblePoppingController;

    void Start()
    {
        bubblePoppingController = GetComponent<BubblePoppingController>();
    }
    
    void FixedUpdate()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x > endX)
        {
            bubblePoppingController.OnBubbleFinishLent();
            Destroy(gameObject);
        }
    }
}