using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticEventHandler
{
    public static event Action<BagRummageEventArgs> BagRummageEvent;
    public static event Action<ExitMiniGameEventArgs> ExitMiniGameEvent;
    public static event Action<ItemSelectedEventArgs> ItemSelectedEvent;
    public static event Action<ConsumableUsedEventArgs> ConsumableUsedEvent;
    public static event Action<ItemDestroyedEventArgs> ItemDestroyedEvent;

    public static void CallBagRummageEvent()
    {
        BagRummageEvent?.Invoke(new BagRummageEventArgs() { });
    }
    public static void CallExitMiniGameEvent()
    {
        ExitMiniGameEvent?.Invoke(new ExitMiniGameEventArgs() { });
    }
    public static void CallItemSelectedEvent(GameObject _backPackObject, ItemDetailsSO _itemDetails)
    {
        ItemSelectedEvent?.Invoke(new ItemSelectedEventArgs(_backPackObject, _itemDetails) { });
    }
    public static void CallConsumableUsedEvent(CharacterStateManager _character)
    {
        ConsumableUsedEvent?.Invoke(new ConsumableUsedEventArgs() {character = _character});
    }

    public static void CallItemDestroyedEvent(GameObject item)
    {
        ItemDestroyedEvent?.Invoke(new ItemDestroyedEventArgs() {item = item});
    }

}

public class BagRummageEventArgs : EventArgs
{
    
}
public class ExitMiniGameEventArgs : EventArgs
{

}

public class ItemSelectedEventArgs : EventArgs
{
    public ItemSelectedEventArgs(GameObject _backPackObject, ItemDetailsSO _itemDetails)
    {
        backPackObject = _backPackObject;
        itemDetails = _itemDetails;
    }
    public GameObject backPackObject;
    public ItemDetailsSO itemDetails;
}

public class ConsumableUsedEventArgs : EventArgs
{
    public CharacterStateManager character;
}

public class ItemDestroyedEventArgs : EventArgs
{
    public GameObject item;
}