using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertMonobehaviour : MonoBehaviour
{

    public List<Sprite> alertSprites = new List<Sprite>();
    private Image image;

    //Colors
    private Color transparent = new Color(0f, 0f, 0f, 0f);

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = transparent;
        image.sprite = alertSprites[0];
    }

    public void InitializeAlert()
    {
        image.color = transparent;
        image.sprite = alertSprites[0];
    }


    public void UpdatePosition(float _x)
    {
        Vector3 newPos = new Vector3(_x, transform.position.y, transform.position.z);
        transform.position = newPos;
    }

    //temporary function for swapping sprites
    public void UpdateSprite(float _health)
    {
        Debug.Log(_health);
        if (_health > 20f && _health <= 40f)
        {
            Debug.Log("Health Low");
            image.sprite = alertSprites[0];
            image.color = Color.white;
        }
        else if(_health > 0f && _health <= 20f)
        {
            Debug.Log("Critical");
            image.sprite = alertSprites[1];
            image.color = Color.white;
        }
        else if(_health <= 0f)
        {
            image.sprite = alertSprites[2];
            image.color = Color.white;
        }
        else
        {
            image.color = transparent;
        }
    }
}
