using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    private Player player;
    private Inventory inventoryComponent;
    private Transform parentTransform;
    private float yOffset;

    public List<InventoryItem> playerInventory = new List<InventoryItem>();
    private List<GameObject> backpackObjects = new List<GameObject>();
    
    private int maxItems = 24;
    private int maxLayerItems = 8;
    private const string bottomLayer = "BPBottom";
    private const string middleLayer = "BPMiddle";
    private const string topLayer = "BPTop";
    private const string offHandLayer = "BPOffHand";
    private const string bottomSortingLayer = "BPBottom";
    private const string middleSortingLayer = "BPMiddle";
    private const string topSortingLayer = "BPTop";
    private const string offHandSortingLayer = "BPOffHand";

    //state//
    [HideInInspector] public bool itemSelected = false;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventoryComponent = GameObject.Find("Player").GetComponent<Inventory>();
        parentTransform = GameObject.Find("BackPackMiniGame").transform;
    }

    private void OnEnable()
    {
        StaticEventHandler.ItemSelectedEvent += RemoveItemFromBackpack;
        StaticEventHandler.ConsumableUsedEvent += useConsumable;
        StaticEventHandler.ItemDestroyedEvent += removeDestroyedItem;
    }

    private void OnDisable()
    {
        StaticEventHandler.ItemSelectedEvent -= RemoveItemFromBackpack;
        StaticEventHandler.ConsumableUsedEvent -= useConsumable;
        StaticEventHandler.ItemDestroyedEvent -= removeDestroyedItem;
    }

    void Start()
    {
        SetYOffset();
        playerInventory = inventoryComponent.inventory;
        //Genereate Items from prefabs
        for (int i = 0; i < maxItems - 4; i++)
        {
            int rand = HelperUtilities.RandInt(0f, (float)playerInventory.Count-1);

            //instantiate the prefab
            GameObject itemPrefab = playerInventory[i].itemDetails.backpackPrefab;
            GameObject item = Instantiate(itemPrefab);

            Item itemComponent = item.GetComponent<Item>();
            ItemDetailsSO selectedItemDetails = playerInventory[i].itemDetails;

            itemComponent.InitializeItem(selectedItemDetails);

            //get a random x and y for starting Position
            float randX = Random.Range(-8f, 8f);
            float randY = Random.Range(-4f, 4f) + yOffset;
            Vector3 startingPos = new Vector3(randX, randY, 0f);
            //set starting position
            item.transform.position = startingPos;

            if (i < maxLayerItems)
            {
                //set the item's layers to bottom
                item.layer = LayerMask.NameToLayer(bottomLayer);
                item.GetComponent<SpriteRenderer>().sortingLayerName = bottomSortingLayer;
            }
            else if (i < maxLayerItems * 2)
            {
                //set the item's layers to middle
                item.layer = LayerMask.NameToLayer(middleLayer);
                item.GetComponent<SpriteRenderer>().sortingLayerName = middleSortingLayer;
            }
            else
            {
                //set the item's layers to top
                item.layer = LayerMask.NameToLayer(topLayer);
                item.GetComponent<SpriteRenderer>().sortingLayerName = topSortingLayer;
            }

            //add reference to the list
            backpackObjects.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReorganizeItemsIntoLayers(GameObject selectedItem)
    {
        backpackObjects.Remove(selectedItem);
        backpackObjects.Add(selectedItem);
        for(int i = 0; i < backpackObjects.Count; i++)
        {
            var item = backpackObjects[i];
            if(i < maxLayerItems)
            {
                //set the item's layers to bottom
                SetItemLayers(item, bottomLayer, bottomSortingLayer);
                SetItemMass(item, 10);
            }
            else if (i < maxLayerItems * 2)
            {
                //set the item's layers to middle
                SetItemLayers(item, middleLayer, middleSortingLayer);
                SetItemMass(item, 5);
            }
            else
            {
                //set the item's layers to top
                SetItemLayers(item, topLayer, topSortingLayer);
                SetItemMass(item, 1);
            }
        }
    }

    public void SetItemLayers(GameObject item, string layerName, string sortingLayerName)
    {
        item.layer = LayerMask.NameToLayer(layerName);
        item.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
    }

    public void SetItemMass(GameObject item, int mass)
    {
        item.GetComponent<Rigidbody2D>().mass = mass;
    }

    public void AddItemToBackpack(GameObject _item)
    {
        backpackObjects.Add(_item);
        DetermineLayer(_item);
        UnlockBackpackItems();

        if (inventoryComponent.GetHandItem() != null)
        {
            //Get Item Details to pass to player inventory
            ItemDetailsSO itemDetails = _item.GetComponent<Item>().itemDetails;
            PutBackHandItem(itemDetails);
        }


    }

    //removes physical game object from the backpack
    public void RemoveItemFromBackpack(ItemSelectedEventArgs eventArgs)
    {
        GameObject _item = eventArgs.backPackObject;
        ItemDetailsSO _itemDetails = eventArgs.itemDetails;

        backpackObjects.Remove(_item);
        SetItemLayers(_item, offHandLayer, offHandSortingLayer);
        LockBackpackItems();

       // SetPlayerInventoryHandItem(_itemDetails);
    }

    public void RemoveItemFromBackpackReg(GameObject _item)
    {
        backpackObjects.Remove(_item);
        SetItemLayers(_item, offHandLayer, offHandSortingLayer);
        LockBackpackItems();

        // SetPlayerInventoryHandItem(_itemDetails);
    }

    public bool IsItemInBackpack(GameObject _item)
    {
        foreach(GameObject obj in backpackObjects)
        {
            if (obj == _item) return true;
        }
        return false;
        
    }

    public void LockBackpackItems()
    {
        for (int i= 0; i < backpackObjects.Count; i++)
        {
            backpackObjects[i].GetComponent<Item>().LockItem();
        }
    }

    public void UnlockBackpackItems()
    {
        for (int i = 0; i < backpackObjects.Count; i++)
        {
            backpackObjects[i].GetComponent<Item>().UnlockItem();
        }
    }

    /*public void RemoveItemFromInventory(GameObject _item)
    {
        string itemName = _item.GetComponent<Item>().itemName;
        inventoryComponent.RemoveItem(itemName);
    }*/

    public void SetPlayerInventoryHandItem(ItemDetailsSO itemDetails)
    {
        //inventoryComponent.SetHandItem(itemDetails);
    }

    public void PutBackHandItem(ItemDetailsSO itemDetails)
    {
        inventoryComponent.PutBackHandItem(itemDetails);
    }

    public void SetYOffset()
    {
        yOffset = parentTransform.position.y;
    }

    public void DetermineLayer(GameObject _item)
    {
        if (backpackObjects.Count < maxLayerItems)
        {
            //set the item's layers to bottom
            _item.layer = LayerMask.NameToLayer(bottomLayer);
            _item.GetComponent<SpriteRenderer>().sortingLayerName = bottomSortingLayer;
        }
        else if (backpackObjects.Count < maxLayerItems * 2)
        {
            //set the item's layers to middle
            _item.layer = LayerMask.NameToLayer(middleLayer);
            _item.GetComponent<SpriteRenderer>().sortingLayerName = middleSortingLayer;
        }
        else
        {
            //set the item's layers to top
            _item.layer = LayerMask.NameToLayer(topLayer);
            _item.GetComponent<SpriteRenderer>().sortingLayerName = topSortingLayer;
        }
    }

    //resets backpack for next use
    public void useConsumable(ConsumableUsedEventArgs eventArgs)
    {
        UnlockBackpackItems();
    }

    public void removeDestroyedItem(ItemDestroyedEventArgs eventArgs)
    {
        RemoveItemFromBackpackReg(eventArgs.item);
    }
}
