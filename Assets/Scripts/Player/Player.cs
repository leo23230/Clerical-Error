using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //this only exists to:
    //give the player an inventory of items
    //store the selected item from the bag
    //allow the player to use the item

    //temp//
    public List<ItemDetailsSO> allItems = new List<ItemDetailsSO>();

    public Health healthComponent;

    [HideInInspector] public List<InventoryItem> inventory = new List<InventoryItem>();

    void Awake()
    {
        healthComponent = GetComponent<Health>();
        healthComponent.SetStartingHealth(50);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
