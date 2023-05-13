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

    public GameObject dropEffect;

    [HideInInspector] public List<InventoryItem> inventory = new List<InventoryItem>();

    void Awake()
    {
        healthComponent = GetComponent<Health>();
        healthComponent.SetStartingHealth(50);
    }

    private void OnEnable()
    {
        StaticEventHandler.ResourceDropEvent += ResourceDropEffect;
    }
    private void OnDisable()
    {
        StaticEventHandler.ResourceDropEvent -= ResourceDropEffect;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public GameObject InstantiateEffectPrefab(GameObject _prefab)
    {
        GameObject effectObject = Instantiate(_prefab);

        //float yOffset = 2.0f;

        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //set pos to middle of character
        effectObject.transform.localPosition = newPos;

        //make sure the effect follows the character
        effectObject.transform.SetParent(transform);
        //spawn effect prefab

        return effectObject;
    }

    public void ResourceDropEffect(ResourceDropEventArgs eventArgs)
    {
        InstantiateEffectPrefab(dropEffect);
    }
}
