using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHand : MonoBehaviour
{

    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFlashRed()
    {
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        Color startingColor = spriteRenderer.color;
        Color red = new Color(255, 0, 0);

        ScreenShake.Instance.ShakeCamera(3f, .05f, false);

        spriteRenderer.color = red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = startingColor;

        yield break;
    }
}
