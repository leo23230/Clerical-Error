using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CancelArea : MonoBehaviour
{
    private GameObject MainButton;
    private Button MainButtonComponent;

    private GameObject BackpackButton;
    private Button BackpackButtonComponent;

    private void Start()
    {
        MainButton = GameObject.Find("MainButton");
        MainButtonComponent = MainButton.GetComponent<Button>();

        BackpackButton = GameObject.Find("BackpackButton");
        BackpackButtonComponent = MainButton.GetComponent<Button>();
    }

    private void OnMouseExit()
    {
        //Debug.Log("OUT");
        MainButtonComponent.interactable = true;
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("IN");
        //MainButtonComponent.interactable = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OUT");
        MainButtonComponent.interactable = true;
    }*/
}
