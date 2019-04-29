using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый класс для логики экипировки.
/// </summary>
public abstract class PlayerEquipmentSlot<EquipmentType> : MonoBehaviour where EquipmentType : ItemAssetEquipment
{
    public EquipmentType itemData;

    public abstract void ActivateEquipment();
}
