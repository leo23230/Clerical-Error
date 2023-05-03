using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CraftingManager : MonoBehaviour
{
    //refs//
    public Transform hotBar;
    public Transform cookingPot;
    private Player player;
    private Inventory inventoryComponent;
    public List<InventoryItem> playerInventory = new List<InventoryItem>();

    [HideInInspector]public List<Transform> inventorySlots = new List<Transform>();
    public List<RecipeSO> craftingRecipes = new List<RecipeSO>();

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventoryComponent = GameObject.Find("Player").GetComponent<Inventory>();

        foreach (Transform child in hotBar)
        {
            inventorySlots.Add(child);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = inventoryComponent.inventory;

        foreach(InventoryItem inventoryItem in playerInventory)
        {
            if (inventoryItem.itemDetails.isIngredient)
            {
                GameObject newCraftingItem = Instantiate(inventoryItem.itemDetails.craftingPrefab);
                DraggableItem draggableItemComponent = newCraftingItem.GetComponent<DraggableItem>();

                //set item count
                draggableItemComponent.InitializeCraftingItem(inventoryItem.itemDetails, inventoryItem.quantity);

                //find an available slot
                Transform availableSlot = FindAvailableInventorySlot();
                
                //put the item in the slot
                if(availableSlot != null)
                {
                    newCraftingItem.transform.position = availableSlot.transform.position;
                    newCraftingItem.transform.SetParent(availableSlot.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            //we want to get a list of the items in the pot in that moment

            DraggableItem[] draggableItems = cookingPot.GetComponentsInChildren<DraggableItem>();

            List<string> craftingItems = new List<string>();

            foreach(DraggableItem draggable in draggableItems)
            {
                craftingItems.Add(draggable.itemDetails.itemName);
            }

            craftingItems.Sort();

            foreach (RecipeSO recipe in craftingRecipes)
            {
                List<string> poop = recipe.GetSortedListOfIngredients();

                if (MatchesRecipe(craftingItems, recipe.GetSortedListOfIngredients()))
                {
                    Debug.Log("Crafted: " + recipe.output.itemName);
                    break;
                }
            }
        }
    }

    public Transform FindAvailableInventorySlot()
    {
        foreach (Transform inventorySlot in inventorySlots)
        {
            if (inventorySlot.childCount == 0)
            {
                return inventorySlot;
            }
        }

        return null;
    }

    private bool MatchesRecipe(List<string> l1, List<string> l2)
    {
        if (l1.Count != l2.Count)
            return false;
        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
                return false;
        }
        return true;
    }

    private void DestroyIngredients()
    {

        //This will respond to an ItemCraftedEvent

        //The inventory component will remove one of each item from the player's inventory

        //Once the ingredients are removed from the inventory,
        //The backpack will be responsible for removing those items

        //Then the crafting game objects will be destroyed in this function
    }
}
