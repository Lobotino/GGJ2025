using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UserGuideScript : MonoBehaviour
{
    public GameObject nextSlide;

    void Start()
    {
        if (FreeGameData.IsFreeGameEnabled)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        if (nextSlide != null)
        {
            nextSlide.SetActive(true);
        }

        Destroy(gameObject);
    }
}