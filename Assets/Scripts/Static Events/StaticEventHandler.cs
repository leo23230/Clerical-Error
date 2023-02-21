using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticEventHandler
{
    public static event Action<BagRummageEventArgs> BagRummageEvent;
    public static event Action<ExitMiniGameEventArgs> ExitMiniGameEvent;

    public static void CallBagRummageEvent()
    {
        BagRummageEvent?.Invoke(new BagRummageEventArgs() { });
    }
    public static void CallExitMiniGameEvent()
    {
        ExitMiniGameEvent?.Invoke(new ExitMiniGameEventArgs() { });
    }

}

public class BagRummageEventArgs : EventArgs
{
    
}
public class ExitMiniGameEventArgs : EventArgs
{

}