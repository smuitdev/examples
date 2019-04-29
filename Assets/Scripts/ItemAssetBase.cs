using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый класс хранящий информацию о какой то вещи.
/// </summary>
[CreateAssetMenu]
public abstract class ItemAssetBase : ScriptableObject
{
    /// <summary>
    /// Уникальный номер вещи.
    /// </summary>
    public int itemId;

    /// <summary>
    /// Название вещи.
    /// </summary>
    public string nickname;
}
