using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventInvoker : MonoBehaviour
{
    //this is simply responsible for allowing UI buttons to invoke static events

    public void callBagRummageEvent()
    {
        StaticEventHandler.CallBagRummageEvent();
    }
    public void callExitMiniGameEvent()
    {
        StaticEventHandler.CallExitMiniGameEvent();
    }
}
