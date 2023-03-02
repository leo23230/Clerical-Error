using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDetails_", menuName = "Scriptable Objects/Character/Character Details")]
public class CharacterDetailsSO : ScriptableObject
{
    #region Header CHARACTER BASE DETAILS
    [Space(10)]
    [Header("CHARACTER BASE DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("Character name.")]
    #endregion
    public string characterName;

    #region Tooltip
    [Tooltip("Prefab gameobject for the character")]
    #endregion
    public GameObject characterPrefab;

    #region Tooltip
    [Tooltip("character runtime animator controller")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;


    #region Header CHARACTER STATS
    [Space(10)]
    [Header("CHARACTER STATS")]
    #endregion
    #region Header HEALTH
    [Space(10)]
    [Header("HEALTH")]
    #endregion
    #region Tooltip
    [Tooltip("Character starting health amount (base 100)")]
    #endregion
    public int characterHealthAmount;

    #region Header SPEED
    [Space(10)]
    [Header("SPEED")]
    #endregion
    #region Tooltip
    [Tooltip("Character speed (1-10) determines turn order")]
    #endregion
    public float characterSpeed;

    #region Header ARMORCLASS
    [Space(10)]
    [Header("ARMORCLASS")]
    #endregion
    #region Tooltip
    [Tooltip("Float (0.5 - 1.0) that is multiplied to incoming damage")]
    #endregion
    public float characterArmorClass;

    #region Header ATTACKRANGE
    [Space(10)]
    [Header("ATTACKRANGE")]
    #endregion
    #region Tooltip
    [Tooltip("Float - max units player can be from target")]
    #endregion
    public float characterAttackRange;

    #region Header CHARACTER ABILITIES
    [Space(10)]
    [Header("CHARACTER ABILITIES")]
    #endregion
    #region Tooltip
    [Tooltip("A list of ability structs")]
    #endregion
    public List<string> characterAbilities;

    #region Header OTHER
    [Space(10)]
    [Header("OTHER")]
    #endregion
    #region Tooltip
    [Tooltip("Character icon sprite to be used in the minimap")]
    #endregion
    public Sprite characterMiniMapIcon;


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(characterName), characterName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(characterPrefab), characterPrefab);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(characterHealthAmount), characterHealthAmount, false);
        //HelperUtilities.ValidateCheckNullValue(this, nameof(characterMiniMapIcon), characterMiniMapIcon);
       // HelperUtilities.ValidateCheckNullValue(this, nameof(runtimeAnimatorController), runtimeAnimatorController);
    }
#endif
    #endregion

}