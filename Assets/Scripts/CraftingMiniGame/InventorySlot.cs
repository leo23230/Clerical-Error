using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (transform.childCount == 0)
        {
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
        else if(transform.childCount >= 1)
        {
            //if it has a slot number, swamp them
            if(transform.GetChild(0).GetComponent<DraggableItem>().itemDetails.name ==
                dropped.GetComponent<DraggableItem>().itemDetails.name)
            {
                transform.GetChild(0).GetComponent<DraggableItem>().itemCount += dropped.GetComponent<DraggableItem>().itemCount;
                Destroy(dropped);
                transform.GetChild(0).GetComponent<DraggableItem>().UpdateCountText();
            }
            //if not its coming from the pot
            //so it will snap to the next available slot
        }
       
    }
}
