using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshProUGUI countText;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public ItemDetailsSO itemDetails;
    private GameObject newDraggableItem;
    public int itemCount = 1;

    private void Awake()
    {
        //countText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void InitializeCraftingItem(ItemDetailsSO _itemDetails, int _count)
    {
        itemDetails = _itemDetails;
        itemCount = _count;

        countText.text = _count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        if (itemCount > 1)
        {
            newDraggableItem = Instantiate(gameObject);
            DraggableItem draggableItemComponent = newDraggableItem.GetComponent<DraggableItem>();

            newDraggableItem.transform.position = transform.position;
            newDraggableItem.transform.localScale = new Vector3(0.0156f,0.0156f,1);
            newDraggableItem.transform.SetParent(transform.parent);

            draggableItemComponent.InitializeCraftingItem(itemDetails, 1);
            draggableItemComponent.UpdateCountText();

            draggableItemComponent.parentAfterDrag = transform.parent;
            //draggableItemComponent.parentAfterDrag.transform.SetParent(transform.root);
            //draggableItemComponent.transform.SetAsLastSibling();
            draggableItemComponent.image.raycastTarget = false;
        }
        else if(itemCount <= 1)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            //transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
        else
        {
            //if it is zero it will skip this step as it is already done//
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(itemCount > 1)
        {
            Vector3 newPos = new Vector3(Input.mousePosition.x * 0.0156f - 15f, (Input.mousePosition.y * 0.0156f) + 9, Input.mousePosition.z);

            newDraggableItem.transform.position = newPos;
        }
        else if(itemCount <= 1)
        {
            //if item count is 1 or 0, it'll follow the mouse during a drag event//

            Vector3 newPos = new Vector3(Input.mousePosition.x * 0.0156f - 15f, (Input.mousePosition.y * 0.0156f) + 9, Input.mousePosition.z);

            transform.position = newPos;
        } 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(itemCount <= 1)
        {
            Debug.Log("End Drag");
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
        else
        {
            if (newDraggableItem != null)
            {
                DraggableItem draggableItemComponent = newDraggableItem.GetComponent<DraggableItem>();

                if (eventData.pointerEnter.name == "CookingGrid")
                {
                    draggableItemComponent.parentAfterDrag = eventData.pointerEnter.transform;
                    newDraggableItem.transform.SetParent(draggableItemComponent.parentAfterDrag);
                }

                if (draggableItemComponent.parentAfterDrag != transform.parent)
                {
                    draggableItemComponent.image.raycastTarget = true;

                    itemCount -= 1;
                    UpdateCountText();
                }
                else
                {
                    Destroy(newDraggableItem);                   
                }
                
            }

            newDraggableItem = null;

        }
    }

    public void UpdateCountText()
    {
        if (itemCount == 0) countText.text = "";
        else countText.text = itemCount.ToString(); 
    }
}
