using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BackpackManager : MonoBehaviour
{
    private Player player;
    private Inventory inventoryComponent;
    private Transform parentTransform;
    private BackpackUIManager BPUIManager;
    private Transform miniGameParent;
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
    private const float bottomScale = 0.8f;
    private const float middleScale = 0.9f;
    private const float topScale = 1.0f;
    private const int bottomMass = 10;
    private const int middleMass = 5;
    private const int topMass = 1;

    //state//
    [HideInInspector] public bool itemSelected = false;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventoryComponent = GameObject.Find("Player").GetComponent<Inventory>();
        parentTransform = GameObject.Find("BackPackMiniGame").transform;
        BPUIManager = GameObject.Find("BackpackUI").GetComponent<BackpackUIManager>();

        miniGameParent = GameObject.Find("BackPackMiniGame").transform;
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

        //A temporary list used to initially add all of the inventory items into the backpack//

        List<ItemDetailsSO> backPackInventory = new List<ItemDetailsSO>();

        foreach(InventoryItem inventoryItem in playerInventory)
        {
            if (inventoryItem.quantity > 1)
            {
                for (int i = 0; i < inventoryItem.quantity; i++)
                {
                    backPackInventory.Add(inventoryItem.itemDetails);
                }
            }
            else
            {
                backPackInventory.Add(inventoryItem.itemDetails);
            }
        }

        //Genereate Items from prefabs
        for (int i = 0; i < backPackInventory.Count; i++)
        {
            //instantiate the prefab
            GameObject itemPrefab = backPackInventory[i].backpackPrefab;
            GameObject item = Instantiate(itemPrefab);

            Item itemComponent = item.GetComponent<Item>();
            ItemDetailsSO selectedItemDetails = backPackInventory[i];

            itemComponent.InitializeItem(selectedItemDetails);

            //get a random x and y for starting Position
            float randX = Random.Range(-8f, 8f);
            float randY = Random.Range(-4f, 4f) + yOffset;
            Vector3 startingPos = new Vector3(randX, randY, 0f);
            //set starting position
            item.transform.position = startingPos;
            item.transform.SetParent(miniGameParent);

            if (i < maxLayerItems)
            {
                //set the item's layers to bottom
                SetItemLayers(item, bottomLayer, bottomSortingLayer);
                SetItemMass(item, bottomMass);
                SetItemScale(item, bottomScale);
            }
            else if (i < maxLayerItems * 2)
            {
                //set the item's layers to middle
                SetItemLayers(item, middleLayer, middleSortingLayer);
                SetItemMass(item, middleMass);
                SetItemScale(item, middleScale);
            }
            else
            {
                //set the item's layers to top
                SetItemLayers(item, topLayer, topSortingLayer);
                SetItemMass(item, topMass);
                SetItemScale(item, topScale);
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

        for (int i = 0; i < backpackObjects.Count; i++)
        {
            var item = backpackObjects[i];
            if(i < maxLayerItems)
            {
                //set the item's layers to bottom
                SetItemLayers(item, bottomLayer, bottomSortingLayer);
                SetItemMass(item, bottomMass);
                SetItemScale(item, bottomScale);
            }
            else if (i < maxLayerItems * 2)
            {
                //set the item's layers to middle
                SetItemLayers(item, middleLayer, middleSortingLayer);
                SetItemMass(item, middleMass);
                SetItemScale(item, middleScale);
            }
            else
            {
                //set the item's layers to top
                SetItemLayers(item, topLayer, topSortingLayer);
                SetItemMass(item, topMass);
                SetItemScale(item, topScale);
            }
        }
    }

    public void DetermineLayer(GameObject _item)
    {
        if (backpackObjects.Count < maxLayerItems)
        {
            //set the item's layers to bottom
            SetItemLayers(_item, bottomLayer, bottomSortingLayer);
            SetItemMass(_item, bottomMass);
            SetItemScale(_item, bottomScale);
        }
        else if (backpackObjects.Count < maxLayerItems * 2)
        {
            //set the item's layers to middle
            SetItemLayers(_item, middleLayer, middleSortingLayer);
            SetItemMass(_item, middleMass);
            SetItemScale(_item, middleScale);
        }
        else
        {
            //set the item's layers to top
            SetItemLayers(_item, topLayer, topSortingLayer);
            SetItemMass(_item, topMass);
            SetItemScale(_item, topScale);
        }
    }

    public void SetItemLayers(GameObject item, string layerName, string sortingLayerName)
    {

        SortingGroup itemSortingGroup = item.GetComponent<Item>().sortingGroup;

        item.layer = LayerMask.NameToLayer(layerName);
        itemSortingGroup.sortingLayerName = sortingLayerName;
    }

    public void SetItemMass(GameObject item, int mass)
    {
        item.GetComponent<Rigidbody2D>().mass = mass;
    }

    public void SetItemScale(GameObject item, float scale)
    {
        Vector3 newScale = new Vector3(scale, scale, item.transform.localScale.z);
        item.transform.localScale = newScale;
    }

    public void AddItemToBackpack(GameObject _item)
    {
        backpackObjects.Add(_item);
        BPUIManager.ResetTooltip();
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
        SetItemScale(_item, bottomScale);

        

        LockBackpackItems();

       // SetPlayerInventoryHandItem(_itemDetails);
    }

    public void RemoveItemFromBackpackReg(GameObject _item)
    {
        backpackObjects.Remove(_item);
        SetItemLayers(_item, offHandLayer, offHandSortingLayer);

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
