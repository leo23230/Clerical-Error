using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameActivator : MonoBehaviour
{
    private bool deactivate = false;

    private void Start()
    {
        StartCoroutine(timedDeactivate());
    }

    private void OnEnable()
    {
        StaticEventHandler.BagRummageEvent += ActivateMiniGame;
        StaticEventHandler.CraftingEvent += ActivateCraftingMiniGame;
        StaticEventHandler.SpellcastingEvent += ActivateSpellcastingMiniGame;
        StaticEventHandler.ExitMiniGameEvent += DeactivateMiniGame;
    }

    private void OnDisable()
    {
        //don't want this to happen//

/*        StaticEventHandler.BagRummageEvent -= ActivateMiniGame;
        StaticEventHandler.CraftingEvent -= ActivateCraftingMiniGame;*/
        StaticEventHandler.ExitMiniGameEvent -= DeactivateMiniGame;
    }

    private void ActivateMiniGame(BagRummageEventArgs eventArgs)
    {
        if(gameObject.name == "BackPackMiniGame")
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }     
    }
    private void ActivateCraftingMiniGame(CraftingEventArgs eventArgs)
    {
        if (gameObject.name == "CraftingMiniGame")
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
    }
    private void ActivateSpellcastingMiniGame(SpellcastingEventArgs eventArgs)
    {
        if (gameObject.name == "SpellCastingMiniGame")
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        //we need to do this at the end of the frame
        //if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    private void DeactivateMiniGame(ExitMiniGameEventArgs eventArgs)
    {
        StartCoroutine(timedDeactivate());
    }

    private IEnumerator timedDeactivate()
    {

        yield return new WaitForSeconds(0.05f);

        if (gameObject.activeSelf) gameObject.SetActive(false);

        yield break;
    }
}
