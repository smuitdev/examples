using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Prophecy.UI
{
    public class DropSlot : MonoBehaviour
    {
        [SerializeField] private int m_DragType;

        public int dragType => m_DragType;

        public static readonly string Tag = "DropSlot";

        public event Action<Draggable> OnDraggableDropped;
    }
}
