using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticEventHandler : MonoBehaviour
{
    public static StaticEventHandler Instance { get; private set; }

    public event Action<BagRummageEventArgs> BagRummageEvent;
    public event Action<CraftingEventArgs> CraftingEvent;
    public event Action<SpellcastingEventArgs> SpellcastingEvent;
    public event Action<ExitMiniGameEventArgs> ExitMiniGameEvent;
    public event Action<ItemSelectedEventArgs> ItemSelectedEvent;
    public event Action<ConsumableUsedEventArgs> ConsumableUsedEvent;
    public event Action<ItemCraftedEventArgs> ItemCraftedEvent;
    public event Action<StartedCraftingEventArgs> StartedCraftingEvent;
    public event Action<ItemDestroyedEventArgs> ItemDestroyedEvent;
    public event Action<EnemySpawnedEventArgs> EnemySpawnedEvent;
    public event Action<EnemyDiedEventArgs> EnemyDiedEvent;
    public event Action<ResourceDropEventArgs> ResourceDropEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void CallBagRummageEvent()
    {
        BagRummageEvent?.Invoke(new BagRummageEventArgs() { });
    }
    public void CallCraftingEvent()
    {
        CraftingEvent?.Invoke(new CraftingEventArgs() { });
    }
    public void CallSpellcastingEvent()
    {
        SpellcastingEvent?.Invoke(new SpellcastingEventArgs() { });
    }
    public void CallExitMiniGameEvent()
    {
        ExitMiniGameEvent?.Invoke(new ExitMiniGameEventArgs() { });
    }
    public void CallItemSelectedEvent(GameObject _backPackObject, ItemDetailsSO _itemDetails)
    {
        ItemSelectedEvent?.Invoke(new ItemSelectedEventArgs(_backPackObject, _itemDetails) { });
    }
    public void CallConsumableUsedEvent(CharacterStateManager _character)
    {
        ConsumableUsedEvent?.Invoke(new ConsumableUsedEventArgs() {character = _character});
    }

    public void CallItemCraftedEvent(List<ItemDetailsSO> _ingredients, ItemDetailsSO _output)
    {
        ItemCraftedEvent?.Invoke(new ItemCraftedEventArgs(_ingredients, _output));
    }

    public void CallStartedCraftingEvent(List<ItemDetailsSO> _ingredients)
    {
        StartedCraftingEvent?.Invoke(new StartedCraftingEventArgs(_ingredients));
    }

    public void CallItemDestroyedEvent(GameObject item)
    {
        ItemDestroyedEvent?.Invoke(new ItemDestroyedEventArgs() {item = item});
    }

    public void CallEnemySpawnedEvent()
    {
        EnemySpawnedEvent?.Invoke(new EnemySpawnedEventArgs() { });
    }

    public void CallEnemyDiedEvent()
    {
        EnemyDiedEvent?.Invoke(new EnemyDiedEventArgs() { });
    }

    public void CallResourceDropEvent()
    {
        ResourceDropEvent?.Invoke(new ResourceDropEventArgs() { });
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

public class ResourceDropEventArgs: EventArgs
{

}
