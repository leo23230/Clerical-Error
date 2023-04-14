using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Item: MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    private SpriteRenderer r;
    [HideInInspector] public SortingGroup sortingGroup;
    private Rigidbody2D rb;
    private BackpackManager backpackManager;
    private GameObject offHand;
    private OffHand offHandComponent;
    private GameObject glow;
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
    private bool isCollidingWithOtherItem = false;
    [HideInInspector] public ItemState state = ItemState.Free;
    private List<Vector3> points = new List<Vector3>();
    private float localSpeed;

    //effects//
    public float maxVelocity = 2f;
    public GameObject breakEffect;
    public GameObject dinkEffect;

    private void Awake()
    {
        r = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sortingGroup = GetComponent<SortingGroup>();
        glow = transform.Find("Glow").gameObject;
        glow.SetActive(false);
    }

    private void Start()
    {
        
        backpackManager = GameObject.Find("BackpackManager").GetComponent<BackpackManager>();
        offHand = GameObject.Find("OffHand");
        offHandComponent = offHand.GetComponent<OffHand>();

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
            points.Add(transform.position);
            //if the item is currently being held, put on hand layer
            gameObject.layer = LayerBackpackHand;
            sortingGroup.sortingLayerName = "BPHand";
        }
        else if (state == ItemState.Released)
        {
/*            Debug.Log("Released");
            Debug.Log(isTouchingOffHand);*/

            if (isTouchingOffHand)
            {
                if (itemDetails.isConsumable)
                {
                    StaticEventHandler.CallItemSelectedEvent(gameObject, itemDetails);
                    //backpackManager.RemoveItemFromBackpack(gameObject, itemDetails);
                    transform.position = offHand.transform.position;
                    transform.rotation = offHand.transform.rotation;
                    SwitchState(ItemState.Selected);
                }
                else
                {
                    transform.position = new Vector3(-8.5f, 13f, transform.position.z);
                    offHandComponent.StartFlashRed();
                    SwitchState(ItemState.Free);
                }
                
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

                //Vector2 newPos = new Vector2(mousePos.x - startPosX, mousePos.y - startPosY);
                //rb.MovePosition(newPos);
                gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);

                if (points.Count < 2) 
                {
                    points.Add(transform.position);
                }
                else
                {
                    points[0] = points[1];
                    points[1] = transform.position;
                }

                localSpeed = Mathf.Abs(Vector3.Distance(points[0], points[1]));
                //Debug.Log(localSpeed);
            }
            else if (state == ItemState.Released)
            {

            }
            else if (state == ItemState.Selected)
            {
                isLocked = false;
                //Debug.Log(isLocked);
            }
        }

       /* Vector3 mousePos;

        mousePos = Input.mousePosition;

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Ray ray = new Ray(mousePos, );

        RaycastHit2D[] results = new RaycastHit2D[10];
        
        RaycastHit2D hit = Physics2D.GetRayIntersectionNonAlloc(ray, results, 10f);*/
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
            //Debug.Log("offhand");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item otherItem = collision.gameObject.GetComponent<Item>();
        if (otherItem != null)
        {
            isCollidingWithOtherItem = true;
            if(otherItem.state == ItemState.Held)
            {
                if (otherItem.localSpeed > maxVelocity)
                {
                    int hitChance = HelperUtilities.RandInt(1f, 5f);

                    if(hitChance == 1)
                    {
                        //break
                        InstantiateEffectPrefab(breakEffect);

                        ScreenShake.Instance.ShakeCamera(10f, .2f, true);

                        StaticEventHandler.CallItemDestroyedEvent(gameObject);

                        Destroy(gameObject);
                    }
                    else
                    {

                        ScreenShake.Instance.ShakeCamera(1f, .1f, false);

                        InstantiateEffectPrefab(dinkEffect);
                    }
                    
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Item>() != null)
        {
            isCollidingWithOtherItem = false;
        }
    }

    //for glow activation
    private void OnMouseEnter()
    {
        if(state != ItemState.Held && !backpackManager.itemSelected)
        {
            glow.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if(glow.activeSelf) glow.SetActive(false);
    }

    public void InstantiateEffectPrefab(GameObject _prefab)
    {
        Debug.Log("making effect");
        GameObject effectObject = Instantiate(_prefab);

        float yOffset = 0f;

        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

        //set pos to middle of character
        effectObject.transform.position = newPos;
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
