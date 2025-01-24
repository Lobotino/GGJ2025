using UnityEngine;

public class TouchInputController : MonoBehaviour
{
    // public GameObject particle;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                // Создаем луч из позиции касания
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Проверяем попадание по 2D-коллайдерам
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null)
                {
                    // Создаем частицу в точке попадания
                    // Instantiate(particle, touchPosition, Quaternion.identity);
                    Debug.Log($"Touched 2D collider at {touchPosition}!");
                    if (hit.collider.CompareTag("Pop"))
                    {
                        hit.collider.gameObject.GetComponent<BubbleTapOnColliderController>().OnTap();
                    }
                }
            }
        }
    }
}