using UnityEngine;

public class BubbleRightLogic : MonoBehaviour
{
    public bool isBubbleRight = false;

    public void OnStart()
    {
        if (isBubbleRight)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}