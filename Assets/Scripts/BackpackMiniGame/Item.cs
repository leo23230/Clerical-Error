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
    private bool stateEntered = false;
    private bool isTouchingOffHand = false;
    [HideInInspector] public ItemState state = ItemState.Free;


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

    private void EnterState()
    {
        if (state == ItemState.Held)
        {
            //if the item is currently being held, put on hand layer
            gameObject.layer = LayerBackpackHand;
            r.sortingLayerName = "BPHand";
        }
        else if (state == ItemState.Released)
        {
            Debug.Log("Released");
            Debug.Log(isTouchingOffHand);
            if (isTouchingOffHand)
            {
                
                backpackManager.RemoveItemFromBackpack(gameObject);
                transform.position = offHand.transform.position;
                transform.rotation = offHand.transform.rotation;
                SwitchState(ItemState.Selected);  
            }
            else
            {
                if (!backpackManager.IsItemInBackpack(gameObject))
                {
                    backpackManager.AddItemToBackpack(gameObject);
                }

                backpackManager.ReorganizeItemsIntoLayers(gameObject);
                transform.rotation = startingRotation;
                SwitchState(ItemState.Free);
            }

        }
        else if (state == ItemState.Selected)
        {

        }

        stateEntered = true;
    }

    private void SwitchState(ItemState _state)
    {
        state = _state;
        stateEntered = false;
    }

    private void Update()
    {
        if (!stateEntered)
        {
            EnterState();
        }
        else
        {
            if (state == ItemState.Held)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
            }
            else if (state == ItemState.Released)
            {

            }
            else if (state == ItemState.Selected)
            {
                isLocked = false;
            }
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

            SwitchState(ItemState.Held);
        }
    }

    private void OnMouseUp()
    {
        if (state == ItemState.Held)
        {
            SwitchState(ItemState.Released);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == offHand)
        {
            Debug.Log("offhand");
            isTouchingOffHand = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //reset the rotation so the player can't reset the rotations of the objects by putting them in the hand
        if (collision.gameObject == offHand)
        {
            isTouchingOffHand = false;
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
}

public enum ItemState
{
    Free,
    Held,
    Selected,
    Released
}
