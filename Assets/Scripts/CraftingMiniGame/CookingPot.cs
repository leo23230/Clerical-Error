using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingPot : MonoBehaviour, IDropHandler
{
    bool isCooking = false;

    private void OnEnable()
    {
        StaticEventHandler.Instance.StartedCraftingEvent += DisableCookingPot;
        StaticEventHandler.Instance.ItemCraftedEvent += EnableCookingPot;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!isCooking)
        {
            //Debug.Log("Item Dropped In Pot");
            if (transform.childCount <= 3)
            {
                GameObject dropped = eventData.pointerDrag;
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                draggableItem.parentAfterDrag = transform;
            }
        }
        
    }

    private void DisableCookingPot(StartedCraftingEventArgs eventArgs)
    {
        isCooking = true;
    }

    private void EnableCookingPot(ItemCraftedEventArgs eventArgs)
    {
        isCooking = false;
    }
}

