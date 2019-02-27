
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Prophecy.UI
{
    public enum ItemGrade
    {
        a1,
        a2,
        a3,
        a4,
        a5,
        a6,
        a7,
        a8,
        a9
    }

    public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private ItemGrade m_ItemGrade;

        public ItemGrade itemGrade
        {
            get => m_ItemGrade;
            set { m_ItemGrade = value; UpdateGradeColor(); }
        }

        private static Color[] GradeColors = {
            Color.gray,

            Color.gray,
            Color.HSVToRGB(200.0f/360.0f,100.0f/100.0f,100.0f/100.0f),
            Color.HSVToRGB(120.0f/360.0f,100.0f/100.0f,70.0f/100.0f),

            Color.gray,                     // Regular
            Color.HSVToRGB(200.0f/360.0f,100.0f/100.0f,100.0f/100.0f),    // Meta
            Color.HSVToRGB(120.0f/360.0f,100.0f/100.0f,70.0f/100.0f),     // Faction
            Color.HSVToRGB(300.0f/360.0f,100.0f/100.0f,100.0f/100.0f),    // Epic
            Color.HSVToRGB(0.0f/360.0f,100.0f/100.0f,100.0f/100.0f)       // Soulbound
        };

        [SerializeField] private bool m_IsDraggable;
        [SerializeField] private int m_DragType;

        [SerializeField] private Image m_BackImage;
        [SerializeField] private Image m_GradeImage;
        [SerializeField] private Image m_IconImage;

        [SerializeField] private DropSlot m_CurrentDropSlot;

        public DropSlot currentDropSlot => m_CurrentDropSlot;

        private Vector3 m_LocalDragPosition;
        private RectTransform m_RectTransform;
        private RectTransform m_ParentOrigin;

        private bool m_IsDragging;

        public void AttachToDropSlot(DropSlot slot)
        {
            m_CurrentDropSlot = slot ?? throw new System.ArgumentNullException();            

            // we need to set parent
            transform.position = m_CurrentDropSlot.transform.position;
            transform.SetParent(m_CurrentDropSlot.transform, true);
            transform.SetAsLastSibling();
        }

        public void DetachFromDropSlot()
        {
            if (m_CurrentDropSlot == null)
                return;

            m_CurrentDropSlot = null;
        }

        private void Start()
        {
            m_RectTransform = GetComponent<RectTransform>();
            m_ParentOrigin = m_RectTransform.parent.GetComponent<RectTransform>();

            if(m_CurrentDropSlot != null)
                AttachToDropSlot(m_CurrentDropSlot);
        }

        private void OnValidate()
        {
            UpdateGradeColor();
        }

        private void UpdateGradeColor()
        {
            if(m_GradeImage)
            {
                m_GradeImage.color = GradeColors[(int)m_ItemGrade];
            }
        }

        /// <summary>
        /// Moves draggable to canvas top so it can fly over other elements easily.
        /// </summary>
        private void MoveToTop()
        {
            m_RectTransform.SetParent(CanvasAccessor.Instance.transform);
            m_RectTransform.SetAsLastSibling();
        }

        /// <summary>
        /// Moves draggable back to previous hierarchy
        /// </summary>
        private void SendToOrigin()
        {
            m_RectTransform.SetParent(m_ParentOrigin);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (!m_IsDraggable)
                return;

            m_IsDragging = true;

            MoveToTop();

            m_LocalDragPosition = m_RectTransform.InverseTransformPoint(Input.mousePosition);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!m_IsDragging)
                return;

            transform.position = Input.mousePosition - m_LocalDragPosition;
           
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (!m_IsDragging)
                return;

            DropSlot newSlot = FindNearestDropSlot(eventData);

            // if NO dragged to slot
            if (newSlot != null && newSlot != m_CurrentDropSlot)
            {
                AttachToDropSlot(newSlot);

                OnDroppedToSlot?.Invoke(newSlot);

                return;
            }
            else
            {
                SendToOrigin();

                m_IsDragging = false;

                if (currentDropSlot != null)
                    transform.position = currentDropSlot.transform.position;
            }
        }

        
        /// <summary>
        /// Locate suitable drop container.
        /// </summaryd>
        /// <param name="eventData"></param>
        /// <returns></returns>
        private DropSlot FindNearestDropSlot(PointerEventData eventData)
        {
            List<RaycastResult> lst = new List<RaycastResult>();
            CanvasAccessor.Raycaster.Raycast(eventData, lst);

            // MH: hack. the first raycasted is always top one (draggable itself)
            // if second one is not dropslot we can say that there's no valid target to drop
            // DropSlot component should be on top of raycasted object.

            if (lst.Count < 2)
                return null;

            return lst[1].gameObject.GetComponentInParent<DropSlot>();
        }

        /// <summary>
        /// Called when this draggable was dropped succesfully to a valid drop slot.
        /// </summary>
        public event System.Action<DropSlot> OnDroppedToSlot;
    }
}
