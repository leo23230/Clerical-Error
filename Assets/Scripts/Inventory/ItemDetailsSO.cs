using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDetails_", menuName = "Scriptable Objects/Inventory/Item Details")]

public class ItemDetailsSO : ScriptableObject
{
    #region Header BASIC ITEM INFO
    [Space(10)]
    [Header("BASIC ITEM INFO")]
    #endregion

    public string itemName;

    public string itemDescription;

    public bool isConsumable;

    public bool isIngredient;

    public bool isTreasure;

    public int maxUses;


    #region Header ITEM PREFABS
    [Space(10)]
    [Header("ITEM PREFABS")]
    #endregion

    public GameObject entityPrefab;

    public GameObject backpackPrefab;

    public GameObject craftingPrefab;
}
