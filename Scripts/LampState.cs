using System.Collections;
using UnityEngine;

public class LampState : MonoBehaviour
{
    //0 - off
    //1 - green
    //2 - red
    //3 - yellow
    public int lampState = 0;

    public Sprite offSprite;
    public Sprite greenSprite;
    public Sprite redSprite;
    public Sprite yellowSprite;

    public void UpdateState(int newState)
    {
        lampState = newState;
        if (newState == 0)
        {
            GetComponent<SpriteRenderer>().sprite = offSprite;
            return;
        }

        if (newState == 1)
        {
            GetComponent<SpriteRenderer>().sprite = greenSprite;
            return;
        }

        if (newState == 2)
        {
            GetComponent<SpriteRenderer>().sprite = redSprite;
            return;
        }

        if (newState == 3)
        {
            GetComponent<SpriteRenderer>().sprite = yellowSprite;
            return;
        }
    }

    public void BlinkYellow()
    {
        StartCoroutine(BlinkLamp(3));
    }


    /// <summary>
    /// Корус для периодического обновления поля.
    /// </summary>
    private IEnumerator BlinkLamp(int color)
    {
        UpdateState(color);
        yield return new WaitForSeconds(0.2f);
        UpdateState(0);
        yield return new WaitForSeconds(0.2f);
        UpdateState(color);
        yield return new WaitForSeconds(0.2f);
        UpdateState(0);
        yield return new WaitForSeconds(0.2f);
        UpdateState(color);
        yield return new WaitForSeconds(0.2f);
        UpdateState(0);
        yield return new WaitForSeconds(0.2f);
    }
}