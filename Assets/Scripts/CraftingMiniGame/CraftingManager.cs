using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    //refs//
    public Transform hotBar;
    public Transform cookingPot;
    public Image CookingTimerBar;
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

    private void OnEnable()
    {
        StaticEventHandler.StartedCraftingEvent += DestroyIngredients;
    }
    private void OnDisable()
    {
        StaticEventHandler.StartedCraftingEvent -= DestroyIngredients;
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

            List<string> craftingItemNames = new List<string>();
            List<ItemDetailsSO> craftingIngredients = new List<ItemDetailsSO>();

            foreach(DraggableItem draggable in draggableItems)
            {
                craftingItemNames.Add(draggable.itemDetails.itemName);
                craftingIngredients.Add(draggable.itemDetails);
            }

            craftingItemNames.Sort();

            foreach (RecipeSO recipe in craftingRecipes)
            {
                List<string> poop = recipe.GetSortedListOfIngredients();

                if (MatchesRecipe(craftingItemNames, recipe.GetSortedListOfIngredients()))
                {
                    Debug.Log("Crafting: " + recipe.output.itemName);
                    StartCoroutine(StartCrafting(craftingIngredients, recipe.output, recipe.craftingDuration));
                    break;
                }
            }
        }
    }

    private IEnumerator StartCrafting(List<ItemDetailsSO> _ingredients, ItemDetailsSO _output, float _craftingDuration)
    {
        float duration = _craftingDuration;
        float normalizedTime = 0;

        StaticEventHandler.CallStartedCraftingEvent(_ingredients);

        while (normalizedTime <= 1f)
        {
            CookingTimerBar.fillAmount = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        StaticEventHandler.CallItemCraftedEvent(_ingredients, _output);

        yield break;
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

    private void DestroyIngredients(StartedCraftingEventArgs eventArgs)
    {

        //This will respond to an ItemCraftedEvent

        for(int i = 0; i < cookingPot.childCount; i++)
        {
            Debug.Log(cookingPot.GetChild(i).name);
            Destroy(cookingPot.GetChild(i).gameObject);
        }

        //The inventory component will remove one of each item from the player's inventory

        //We must disable the hot bar and crockpot

        //Once the ingredients are removed from the inventory,
        //The backpack will be responsible for removing those items

        //Then the crafting game objects will be destroyed in this function
    }
}
