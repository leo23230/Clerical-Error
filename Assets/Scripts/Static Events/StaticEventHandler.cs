using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticEventHandler
{
    public static event Action<BagRummageEventArgs> BagRummageEvent;
    public static event Action<CraftingEventArgs> CraftingEvent;
    public static event Action<SpellcastingEventArgs> SpellcastingEvent;
    public static event Action<ExitMiniGameEventArgs> ExitMiniGameEvent;
    public static event Action<ItemSelectedEventArgs> ItemSelectedEvent;
    public static event Action<ConsumableUsedEventArgs> ConsumableUsedEvent;
    public static event Action<ItemCraftedEventArgs> ItemCraftedEvent;
    public static event Action<StartedCraftingEventArgs> StartedCraftingEvent;
    public static event Action<ItemDestroyedEventArgs> ItemDestroyedEvent;
    public static event Action<EnemySpawnedEventArgs> EnemySpawnedEvent;
    public static event Action<EnemyDiedEventArgs> EnemyDiedEvent;


    public static void CallBagRummageEvent()
    {
        BagRummageEvent?.Invoke(new BagRummageEventArgs() { });
    }
    public static void CallCraftingEvent()
    {
        CraftingEvent?.Invoke(new CraftingEventArgs() { });
    }
    public static void CallSpellcastingEvent()
    {
        SpellcastingEvent?.Invoke(new SpellcastingEventArgs() { });
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

    public static void CallItemCraftedEvent(List<ItemDetailsSO> _ingredients, ItemDetailsSO _output)
    {
        ItemCraftedEvent?.Invoke(new ItemCraftedEventArgs(_ingredients, _output));
    }

    public static void CallStartedCraftingEvent(List<ItemDetailsSO> _ingredients)
    {
        StartedCraftingEvent?.Invoke(new StartedCraftingEventArgs(_ingredients));
    }

    public static void CallItemDestroyedEvent(GameObject item)
    {
        ItemDestroyedEvent?.Invoke(new ItemDestroyedEventArgs() {item = item});
    }

    public static void CallEnemySpawnedEvent()
    {
        EnemySpawnedEvent?.Invoke(new EnemySpawnedEventArgs() { });
    }

    public static void CallEnemyDiedEvent()
    {
        EnemyDiedEvent?.Invoke(new EnemyDiedEventArgs() { });
    }

}

public class BagRummageEventArgs : EventArgs
{
    
}
public class SpellcastingEventArgs : EventArgs
{

}
public class CraftingEventArgs : EventArgs
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

public class ItemCraftedEventArgs : EventArgs
{
    public ItemCraftedEventArgs(List<ItemDetailsSO> _ingredients, ItemDetailsSO _output)
    {
        ingredients = _ingredients;
        output = _output;
    }
    public List<ItemDetailsSO> ingredients;
    public ItemDetailsSO output;
}

public class StartedCraftingEventArgs : EventArgs
{
    public StartedCraftingEventArgs(List<ItemDetailsSO> _ingredients)
    {
        ingredients = _ingredients;
    }
    public List<ItemDetailsSO> ingredients;
}

public class ItemDestroyedEventArgs : EventArgs
{
    public GameObject item;
}

public class EnemySpawnedEventArgs : EventArgs
{
    
}

public class EnemyDiedEventArgs : EventArgs
{

}