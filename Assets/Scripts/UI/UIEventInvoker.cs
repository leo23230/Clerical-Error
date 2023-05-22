using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventInvoker : MonoBehaviour
{
    //this is simply responsible for allowing UI buttons to invoke static events

    public void callBagRummageEvent()
    {
        StaticEventHandler.Instance.CallBagRummageEvent();
    }
    public void callCrafitngEvent()
    {
        StaticEventHandler.Instance.CallCraftingEvent();
    }
    public void callSpellcastingEvent()
    {
        StaticEventHandler.Instance.CallSpellcastingEvent();
    }
    public void callExitMiniGameEvent()
    {
        StaticEventHandler.Instance.CallExitMiniGameEvent();
    }
}
