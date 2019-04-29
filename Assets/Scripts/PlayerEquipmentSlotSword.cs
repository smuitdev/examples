using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentSlotSword : PlayerEquipmentSlot<ItemAssetEquipmentSword>
{
    public override void ActivateEquipment()
    {
        ItemAssetEquipmentSword swordData = itemData;
    }
}
