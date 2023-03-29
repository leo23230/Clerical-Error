using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemDetailsSO> allItems = new List<ItemDetailsSO>();
    private ItemDetailsSO itemInHand;

    [HideInInspector] public List<InventoryItem> inventory = new List<InventoryItem>();

    private void Awake()
    {
        InitializeInventory();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void InitializeInventory()
    {
        //populate this inventory with 24 random objects
        int numItems = 24;
        for (int i = 0; i < numItems; i++)
        {
            //Get a random ItemSO
            int rand = HelperUtilities.RandInt(0f, allItems.Count - 1);
            ItemDetailsSO randItem = allItems[rand];

            //Create an inventory item from it
            InventoryItem item = new InventoryItem();
            item.itemDetails = randItem;
            item.quantity = 1;

            //add the inventory item
            inventory.Add(item);
        }
    }

    public void AddItem(ItemDetailsSO _item)
    {
        //if the item exists, just increase quantity.
        //search for the item in the player's inventory, and remove it

        //this is for the future

        /*for (int i = 0; i < inventory.Count; i++)
        {
            InventoryItem inventoryItem = inventory[i];

            if (inventoryItem.itemDetails.name == _item.name)
            {
                    inventoryItem.quantity += 1;
            }
            else
            {
                InventoryItem newInventoryItem = new InventoryItem();
                newInventoryItem.itemDetails = _item;
                newInventoryItem.quantity = 1;
                inventory.Add(newInventoryItem);
            }
        }*/
        InventoryItem newInventoryItem = new InventoryItem();
        newInventoryItem.itemDetails = _item;
        newInventoryItem.quantity = 1;
        inventory.Add(newInventoryItem);
    }

    public void RemoveItem(string _itemName)
    {
        //search for the item in the player's inventory, and remove it
        for (int i = 0; i < inventory.Count; i++)
        {
            InventoryItem inventoryItem = inventory[i];

            if (inventoryItem.itemDetails.name == _itemName)
            {
                if (inventoryItem.quantity > 1)
                {
                    inventoryItem.quantity -= 1;
                }
                else
                {
                    inventory.Remove(inventoryItem);
                }
            }
        }
    }

    public void SetHandItem(ItemDetailsSO _item)
    {
        //if the player is holding an item, put it back into the inventory
        if(itemInHand == null)
        {
            itemInHand = _item;
        }
        else
        {
            RemoveItem(_item.name);
            itemInHand = _item;
        }
    }

    public void PutBackHandItem(ItemDetailsSO _item)
    {
        //if the player is holding an item, put it back into the inventory
        if (itemInHand != null)
        {
            AddItem(_item);
            itemInHand = null;
        }
    }

    public ItemDetailsSO GetHandItem()
    {
        return itemInHand;
    }

    public void UseHandItem()
    {
        //use the item//
        itemInHand = null;
    }
}
