using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private GameObject MainButton;
    private MainButton MainButtonComponent;

    private void OnEnable()
    {
        StaticEventHandler.Instance.BagRummageEvent += ActivateSelf;
        StaticEventHandler.Instance.CraftingEvent += ActivateSelf;
        StaticEventHandler.Instance.SpellcastingEvent += ActivateSelfSpellcasting;
        StaticEventHandler.Instance.ExitMiniGameEvent += DeactivateSelf;
    }

    private void OnDisable()
    {
/*        StaticEventHandler.BagRummageEvent -= ActivateSelf;
        StaticEventHandler.CraftingEvent -= ActivateSelf;*/
        //StaticEventHandler.Instance.ExitMiniGameEvent -= DeactivateSelf;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainButton = GameObject.Find("MainButton");
        MainButtonComponent = MainButton.GetComponent<MainButton>();
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisableButtonObject()
    {
        MainButton.SetActive(true);  
    }

    public void DeactivateSelf(ExitMiniGameEventArgs eventArgs)
    {
        gameObject.SetActive(false);
    }

    public void ActivateSelf(BagRummageEventArgs backpackArgs)
    {
        gameObject.SetActive(true);
    }
    public void ActivateSelf(CraftingEventArgs craftingArgs)
    {
        gameObject.SetActive(true);
    }
    public void ActivateSelfSpellcasting(SpellcastingEventArgs eventArgs)
    {
        //Debug.Log("GAHHHHHHHHH");
        gameObject.SetActive(true);
    }
}
