using UnityEngine;

public class MoveOnLentAndDestroyOnFinish : MonoBehaviour
{
    public float speed = 3f;
    
    // Позиция, где объект исчезает (справа)
    public float endX = 15f;

    void FixedUpdate()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x > endX)
        {
            Destroy(gameObject);
        }
    }
}