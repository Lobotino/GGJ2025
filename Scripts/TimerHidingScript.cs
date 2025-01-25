using UnityEngine;

public class TimerHidingScript : MonoBehaviour
{
    public bool isForFreeMode = false;

    void Start()
    {
        if (!isForFreeMode && FreeGameData.IsFreeGameEnabled || isForFreeMode && !FreeGameData.IsFreeGameEnabled)
        {
            gameObject.SetActive(false);
        }
    }
}