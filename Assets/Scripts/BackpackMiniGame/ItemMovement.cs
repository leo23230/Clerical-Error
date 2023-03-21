using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMovement : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isHeld = false;
    private SpriteRenderer r;
    private Rigidbody2D rb;

    private int LayerBackpackTop;
    private int LayerBackpackMiddle;
    private int LayerBackpackBottom;

    private void Start()
    {
        r = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        LayerBackpackTop = LayerMask.NameToLayer("BackpackLayerTop");
        LayerBackpackMiddle = LayerMask.NameToLayer("BackpackLayerMiddle");
        LayerBackpackBottom = LayerMask.NameToLayer("BackpackLayerBottom");
    }

    private void Update()
    {
        if (isHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isHeld = true;

            gameObject.layer = LayerBackpackTop;
            r.sortingLayerName = "BPFront";
        }
    }

    private void OnMouseUp()
    {
        isHeld = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (r.sortingOrder == 1)
        {
            r = collision.gameObject.GetComponent<SpriteRenderer>();
            if (r.sortingOrder == 2 || r.sortingOrder == 3)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        if (r.sortingOrder == 2)
        {
            r = collision.gameObject.GetComponent<SpriteRenderer>();
            if (r.sortingOrder == 1 || r.sortingOrder == 3)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        if (r.sortingOrder == 3)
        {
            r = collision.gameObject.GetComponent<SpriteRenderer>();
            if (r.sortingOrder == 2 || r.sortingOrder == 1)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }*/
    }
}
