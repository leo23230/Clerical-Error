using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item: MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isHeld = false;
    private SpriteRenderer r;
    private Rigidbody2D rb;
    private BackpackManager backpackManager;
    private GameObject offHand;
    //we need to get the hand object
    //if we're colliding with it, then it is good, else, no good.

    [HideInInspector] public ItemDetailsSO itemDetails;
    [HideInInspector] public string itemName;

    private int LayerBackpackTop;
    private int LayerBackpackMiddle;
    private int LayerBackpackBottom;
    private int LayerBackpackHand;

    private Quaternion startingRotation;
    private bool isLocked = false;
    private bool isSelected = false;

    private void Start()
    {
        r = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        backpackManager = GameObject.Find("BackpackManager").GetComponent<BackpackManager>();
        offHand = GameObject.Find("OffHand");

        float randRotation = Random.Range(0f, 360f);

        //we need to store this for later use
        startingRotation = Quaternion.Euler(0f, 0f, randRotation);
        transform.rotation = startingRotation;
        

        LayerBackpackTop = LayerMask.NameToLayer("BPTop");
        LayerBackpackMiddle = LayerMask.NameToLayer("BPMiddle");
        LayerBackpackBottom = LayerMask.NameToLayer("BPBottom");
        LayerBackpackHand = LayerMask.NameToLayer("BPHand");
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

    public void InitializeItem(ItemDetailsSO _details)
    {
        itemDetails = _details;
        itemName = itemDetails.itemName;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isLocked)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isHeld = true;

            //if the item is currently being held, put on hand layer
            gameObject.layer = LayerBackpackHand;
            r.sortingLayerName = "BPHand";
        }
    }

    private void OnMouseUp()
    {
        if (isHeld)
        {
            //if the item is selected, we don't want to reorganize the layers.
            if (!isSelected)
            {
                backpackManager.ReorganizeItemsIntoLayers(gameObject);
            }
            isHeld = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == offHand && isHeld)
        {
            backpackManager.RemoveItemFromBackpack(gameObject);
            isSelected = true;
            transform.position = offHand.transform.position;
            transform.rotation = offHand.transform.rotation;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //reset the rotation so the player can't reset the rotations of the objects by putting them in the hand
        if (collision.gameObject == offHand)
        {
            backpackManager.AddItemToBackpack(gameObject);
            transform.rotation = startingRotation;
            isSelected = false;
        }
    }

    public void LockItem()
    {
        isLocked = true;
    }

    public void UnlockItem()
    {
        isLocked = false;
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
