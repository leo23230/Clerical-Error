using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/Enemy Details")]

public class EnemyDetailsSO : ScriptableObject
{
    #region Header enemy BASE DETAILS
    [Space(10)]
    [Header("ENEMY BASE DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("enemy name.")]
    #endregion
    public string enemyName;

    #region Tooltip
    [Tooltip("Prefab gameobject for the enemy")]
    #endregion
    public GameObject enemyPrefab;

    #region Tooltip
    [Tooltip("enemy runtime animator controller")]
    #endregion
    public RuntimeAnimatorController runtimeAnimatorController;


    #region Header enemy STATS
    [Space(10)]
    [Header("enemy STATS")]
    #endregion
    #region Header HEALTH
    [Space(10)]
    [Header("HEALTH")]
    #endregion
    #region Tooltip
    [Tooltip("enemy starting health amount (base 100)")]
    #endregion
    public int enemyHealthAmount;

    #region Header SPEED
    [Space(10)]
    [Header("SPEED")]
    #endregion
    #region Tooltip
    [Tooltip("enemy speed (1-10) determines turn order")]
    #endregion
    public float enemySpeed;

    #region Header ARMORCLASS
    [Space(10)]
    [Header("ARMORCLASS")]
    #endregion
    #region Tooltip
    [Tooltip("Float (0.5 - 1.0) that is multiplied to incoming damage")]
    #endregion
    public float enemyArmorClass;

    #region Header ATTACKRANGE
    [Space(10)]
    [Header("ATTACKRANGE")]
    #endregion

    #region Tooltip
    [Tooltip("Float - max units player can be from target")]
    #endregion
    public float enemyAttackMax;

    #region Tooltip
    [Tooltip("Float - min units player can be from target")]
    #endregion
    public float enemyAttackMin;

    #region Header ABILITYCOOLDOWN
    [Space(10)]
    [Header("ATTACK COOLDOWN")]
    #endregion

    #region Tooltip
    [Tooltip("Float - how long the enemy spends between attacks")]
    #endregion
    public float enemyAbilityCooldown;
}
