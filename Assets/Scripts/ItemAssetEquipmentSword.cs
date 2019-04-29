using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemAssetEquipmentSword : ItemAssetEquipment
{
    public int damage;

    public override EquipType equipType
    {
        get
        {
            return EquipType.Sword;
        }
    }

}
