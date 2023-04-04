using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainButton : MonoBehaviour
{

    GameObject BackpackButton;
    GameObject SpellcastingButton;
    GameObject CraftingButton;
    private Button BackpackButtonComponent;
    private Button SpellcastingButtonComponent;
    private Button CraftingButtonComponent;
    private Button MainButtonComponent;

    //animation triggers//
    private string normal;
    private string highlighted;
    private string selected;

    private float deactivateTimer = 0f;
    private float deactivateTimerSet = 0.2f;

    private void OnEnable()
    {
        StaticEventHandler.BagRummageEvent += StartTimedDeactivate;
        StaticEventHandler.ExitMiniGameEvent += ActivateSelf;
    }

    private void OnDisable()
    {
        //StaticEventHandler.BagRummageEvent -= StartTimedDeactivate;
        //StaticEventHandler.ExitMiniGameEvent -= ActivateSelf;
    }

    private void Start()
    {
        BackpackButton = GameObject.Find("BackpackButton");
        SpellcastingButton = GameObject.Find("SpellcastingButton");
        CraftingButton = GameObject.Find("CraftingButton");

        BackpackButtonComponent = BackpackButton.GetComponent<Button>();
        SpellcastingButtonComponent = SpellcastingButton.GetComponent<Button>();
        CraftingButtonComponent = CraftingButton.GetComponent<Button>();
        MainButtonComponent = GetComponent<Button>();
    }

    public void ActivateActionButtons()
    {
        BackpackButton.SetActive(true);
        SpellcastingButton.SetActive(true);
        CraftingButton.SetActive(true);
        MainButtonComponent.interactable = false;
    }

    public void DeactivateActionButtons()
    {
        BackpackButton.SetActive(false);
        SpellcastingButton.SetActive(false);
        CraftingButton.SetActive(false);
        MainButtonComponent.interactable = true;
    }

    private IEnumerator TimedDeactivate()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        yield break;
    }

    private IEnumerator TimedActivate()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(true);
        yield break;
    }

    public void StartTimedDeactivate(BagRummageEventArgs eventArgs)
    {
        StartCoroutine(TimedDeactivate());
    }

    public void ActivateSelf(ExitMiniGameEventArgs eventArgs)
    {
        gameObject.SetActive(true);
    }

/*    public void StartTimedActivate()
    {
        StartCoroutine(TimedActivate());
    }*/
   
}
