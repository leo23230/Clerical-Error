using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClosedPotLid : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    public GameObject fireEffect;

    public GameObject openLid;
    public GameObject closedLid;
    public Vector3 closedPosition = new Vector3();
    public Vector3 openedPosition = new Vector3();
    private CraftingManager craftingManager;

    private void Awake()
    {
        craftingManager = GameObject.Find("CraftingManager").GetComponent<CraftingManager>();

        fireEffect.SetActive(false);
    }
    private void OnEnable()
    {
        StaticEventHandler.StartedCraftingEvent += EnableLid;
        StaticEventHandler.ItemCraftedEvent += DisableLid;
    }
    private void OnDisable()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        craftingManager.InitializeCraft();
    }

    void EnableLid(StartedCraftingEventArgs eventArgs)
    {
        closedLid.SetActive(true);
        openLid.SetActive(false);

        transform.position = closedPosition;
        fireEffect.SetActive(true);
    }
    void DisableLid(ItemCraftedEventArgs eventArgs)
    {
        closedLid.SetActive(false);
        openLid.SetActive(true);

        transform.position = openedPosition;
        fireEffect.SetActive(false);
    }
}
