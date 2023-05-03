using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Recipe_", menuName = "Scriptable Objects/Crafting/Recipe")]
public class RecipeSO : ScriptableObject
{
    #region Header OUTPUT OF RECIPE
    [Space(10)]
    [Header("OUTPUT OF RECIPE")]
    #endregion
    public ItemDetailsSO output;

    #region Header INGREDIENTS FOR RECIPE
    [Space(10)]
    [Header("INGREDIENTS FOR RECIPE")]
    #endregion

    public ItemDetailsSO ingredient01;

    public ItemDetailsSO ingredient02;

    public ItemDetailsSO ingredient03;

    public ItemDetailsSO ingredient04;

    public float craftingDuration;

    public List<string> GetSortedListOfIngredients()
    {
        List<string> list = new List<string>();

        if(ingredient01 != null) list.Add(ingredient01.itemName);
        if(ingredient02 != null) list.Add(ingredient02.itemName);
        if(ingredient03 != null) list.Add(ingredient03.itemName);
        if(ingredient04 != null) list.Add(ingredient04.itemName);

        list.Sort();

        return list;
    }
    //need to get a count of certain items
}
