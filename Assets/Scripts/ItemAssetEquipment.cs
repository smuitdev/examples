using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый класс для вещей которые можно одеть на игрока.
/// </summary>
public abstract class ItemAssetEquipment : ItemAssetBase
{
    /// <summary>
    /// Тип вещи которую можно одеть
    /// </summary>
    public enum EquipType
    {
        Sword,
        Shield,
        Ring
    }

    /// <summary>
    /// Свойство что это за вещь.
    /// </summary>
    public abstract EquipType equipType { get; }
}
