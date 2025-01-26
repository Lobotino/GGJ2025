using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UserGuideScript : MonoBehaviour
{
    public float timerInSeconds = 6;

    void Start()
    {
        if (FreeGameData.IsFreeGameEnabled)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(DestroyWithDelay());
    }

    // Update is called once per frame
    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(timerInSeconds);
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}