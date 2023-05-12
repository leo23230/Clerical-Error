using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkBottle : MonoBehaviour
{
    public float inkMax = 3;
    [HideInInspector] public float inkAmt = 0;
    public Sprite empty;
    public Sprite almostEmpty;
    public Sprite almostFull;
    public Sprite full;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        inkAmt = inkMax;
    }

    public void UpdateInkSprite()
    {
        Sprite newSprite = full;

        if (Mathf.Round(inkAmt) == inkMax) newSprite = full;
        else if (Mathf.Round(inkAmt) == Mathf.Round(inkMax*2/3)) newSprite = almostFull;
        else if (Mathf.Round(inkAmt) == Mathf.Round(inkMax*1/3)) newSprite = almostEmpty;
        else if (Mathf.Round(inkAmt) == 0) newSprite = empty;

        image.sprite = newSprite;
    }

}
