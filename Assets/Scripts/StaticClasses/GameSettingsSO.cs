using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings_", menuName = "Scriptable Objects/Settings/Game Settings")]

public class GameSettingsSO : ScriptableObject
{
    #region Header CHARACTER BASE DETAILS
    [Space(10)]
    [Header("CHARACTER AND ENEMY SETTINGS")]
    #endregion

    #region Tooltip
    [Tooltip("scales damage of characters")]
    #endregion
    public float characterDamageScale;

    #region Tooltip
    [Tooltip("scales damage of characters")]
    #endregion
    public float enemyDamageScale;
}
