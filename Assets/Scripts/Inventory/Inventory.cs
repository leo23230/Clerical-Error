using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemDetailsSO> allItems = new List<ItemDetailsSO>();
    private ItemDetailsSO itemInHand;
    [HideInInspector] public List<string> preparedSpell = new List<string>();
    private GameObject handItemBackpackObject;

    [HideInInspector] public List<InventoryItem> inventory = new List<InventoryItem>();
    private BackpackManager backpackManager;

    private void Awake()
    {
        InitializeInventory(30);
        PrintInventory();
    }

    void Start()
    {
       backpackManager = GameObject.Find("BackpackManager").GetComponent<BackpackManager>();
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        StaticEventHandler.ItemSelectedEvent += SetHandItem;
        StaticEventHandler.ConsumableUsedEvent += UseHandItem;
        StaticEventHandler.StartedCraftingEvent += RemoveUsedCraftingIngredients;
        StaticEventHandler.ItemDestroyedEvent += RemoveDestroyedItem;
    }

    private void OnDisable()
    {
        StaticEventHandler.ItemSelectedEvent -= SetHandItem;
        StaticEventHandler.ConsumableUsedEvent -= UseHandItem;
        StaticEventHandler.StartedCraftingEvent -= RemoveUsedCraftingIngredients;
        StaticEventHandler.ItemDestroyedEvent -= RemoveDestroyedItem;
    }

    void InitializeInventory(int numItems)
    {
        
        //populate this inventory with 24 random objects
        for (int i = 0; i < numItems; i++)
        {

            //If not atleast 2 of each consumable, Get a random ItemSO
            int healAmt = 0;
            int speedAmt = 0;
            int damageAmt = 0;
            int salveAmt = 0;
            int yukaAmt = 0;
            int pepperAmt = 0;
            int petalAmt = 0;
            int beadAmt = 0;

            foreach (InventoryItem inventoryItem in inventory)
            {
                if (inventoryItem.itemDetails.itemName == "Coppabloom Tea") healAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Papariko Incense") speedAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Slayer Stew") damageAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Herbal Salve") salveAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Yuka Sprigs") yukaAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Papariko Peppercorns") pepperAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Coppabloom Petals") petalAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Starly Beads") beadAmt = inventoryItem.quantity;
            }

            //Debug.Log(healAmt);

            ItemDetailsSO randItem;

            if (healAmt < 3)
            {
                randItem = allItems[3];
            }
            else if (speedAmt < 2)
            {
                randItem = allItems[6];
            }
            else if (damageAmt < 2)
            {
                randItem = allItems[5];
            }
            else if (salveAmt < 2)
            {
                randItem = allItems[4];
            }
            else if (yukaAmt < 3)
            {
                randItem = allItems[1];
            }
            else if(petalAmt < 1)
            {
                randItem = allItems[0];
            }
            else if(pepperAmt < 2)
            {
                randItem = allItems[2];
            }
            else if (beadAmt < 1)
            {
                randItem = allItems[7];
            }
            else
            {
                int rand = HelperUtilities.RandInt(0f, allItems.Count - 1);
                randItem = allItems[rand];
            }
            /*int rand = HelperUtilities.RandInt(0f, allItems.Count - 1);
            ItemDetailsSO randItem = allItems[rand];*/

            AddItem(randItem);
        }
    }

    public void AddMoreCraftingIngredients(int numItems)
    {
        for (int i = 0; i < numItems; i++)
        {
            int yukaAmt = 0;
            int pepperAmt = 0;
            int petalAmt = 0;
            int beadAmt = 0;

            foreach (InventoryItem inventoryItem in inventory)
            {
                if (inventoryItem.itemDetails.itemName == "Yuka Sprigs") yukaAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Papariko Peppercorns") pepperAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Coppabloom Petals") petalAmt = inventoryItem.quantity;
                if (inventoryItem.itemDetails.itemName == "Starly Beads") beadAmt = inventoryItem.quantity;
            }

            //Debug.Log(healAmt);

            ItemDetailsSO randItem;

            if (yukaAmt < 3)
            {
                randItem = allItems[1];
            }
            else if (petalAmt < 1)
            {
                randItem = allItems[0];
            }
            else if (pepperAmt < 2)
            {
                randItem = allItems[2];
            }
            else if (beadAmt < 2)
            {
                randItem = allItems[7];
            }
            else
            {
                int rand = HelperUtilities.RandInt(0f, 2f);
                randItem = allItems[rand];
            }
            /*int rand = HelperUtilities.RandInt(0f, allItems.Count - 1);
            ItemDetailsSO randItem = allItems[rand];*/

            AddItem(randItem);
            backpackManager.InstantiateBackpackObject(randItem.backpackPrefab, randItem);
        }
    }

    public void AddItem(ItemDetailsSO _item)
    {
        //if the item exists, just increase quantity.
        //search for the item in the player's inventory, and remove it

        //this is for the future

        for (int i = 0; i < inventory.Count; i++)
        {
            InventoryItem inventoryItem = inventory[i];

            if (inventoryItem.itemDetails.name == _item.name)
            {

                //only because I used structs and i cannot directly change the quantity on it through the list
                InventoryItem updatedInventoryItem = new InventoryItem();
                updatedInventoryItem.itemDetails = _item;
                updatedInventoryItem.quantity = inventoryItem.quantity += 1;

                inventory[i] = updatedInventoryItem;
                return;
            }
        }

        //if the item is not incremented, it is added here as a new entry/
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

            if (inventoryItem.itemDetails.itemName == _itemName)
            {
                if (inventoryItem.quantity > 1)
                {
                    InventoryItem updatedItem = new InventoryItem();
                    updatedItem.itemDetails = inventoryItem.itemDetails;
                    updatedItem.quantity = inventoryItem.quantity -= 1;

                    inventory[i] = updatedItem;

                    break;
                }
                else
                {
                    inventory.Remove(inventoryItem);
                    break;
                }
            }
        }
    }

    public void RemoveDestroyedItem(ItemDestroyedEventArgs eventArgs)
    {
        Item itemComponent = eventArgs.item.GetComponent<Item>();
        RemoveItem(itemComponent.itemName);

        PrintInventory();
    }

    public void SetHandItem(ItemSelectedEventArgs eventArgs)
    {
        GameObject _backpackObject = eventArgs.backPackObject;
        ItemDetailsSO _itemDetails = eventArgs.itemDetails;

        //if the player is holding an item, put it back into the inventory
        if (itemInHand == null)
        {
            itemInHand = _itemDetails;
            handItemBackpackObject = _backpackObject;
            Debug.Log(_itemDetails.itemName);
        }
        else
        {
            AddItem(_itemDetails);
            itemInHand = _itemDetails;
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

    public bool hasHandItem()
    {
        return itemInHand != null;
    }

    public void UseHandItem(ConsumableUsedEventArgs eventArgs)
    {
        CharacterStateManager character = eventArgs.character;
        //use the item//
        if (character.NotDead() && itemInHand.isConsumable)
        {
            Debug.Log("Used Hand item");
            character.StatBoost(itemInHand);
        }

        //Make sure item is no longer in backpack, and unlock the backpack items.
        Destroy(handItemBackpackObject);

        //reset variables
        itemInHand = null;
        handItemBackpackObject = null;
    }

    private void PrintInventory()
    {
        foreach (InventoryItem item in inventory)
        {
            Debug.Log(item.itemDetails.name + ": " + item.quantity);
        }
    }

    private void RemoveUsedCraftingIngredients(StartedCraftingEventArgs eventArgs)
    {
        foreach(ItemDetailsSO ingredient in eventArgs.ingredients)
        {
            Debug.Log("Removing " + ingredient.itemName + " from inventory");
            RemoveItem(ingredient.itemName);
        }
        PrintInventory();
    }

    //spellcasting

    public void SetPreparedSpell(List<string> _spell)
    {
        preparedSpell = _spell;
    }
    public List<string> GetPreparedSpell()
    {
        return preparedSpell;
    }
    public void UsePreparedSpell()
    {
        preparedSpell = new List<string>();
    }
    public bool hasPreparedSpell()
    {
        return preparedSpell.Count != 0;
    }

}
