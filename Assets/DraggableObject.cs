using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject :
    MonoBehaviour,
    IDragHandler,
    IEndDragHandler,
    IBeginDragHandler
{
    [SerializeField] private Image m_ImageBack;
    [SerializeField] private Image m_ImageBorder;
    [SerializeField] private Image m_ImageIcon;

    public void SetImages(Sprite borderImage, Sprite backImage, Sprite icon)
    {
        m_ImageBack.sprite = backImage;
        m_ImageBorder.sprite = borderImage;
        m_ImageIcon.sprite = icon;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        MoveToTop();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        DropSlotObject dropSlot = FindNearestDropSlot(eventData);

        if (dropSlot != null && dropSlot != m_CurrentDropSlot)
        {
            AttachToDropSlot(dropSlot);
        }
        else
        {
            SendToOrigin();

            if (m_CurrentDropSlot != null)
                transform.position = m_CurrentDropSlot.transform.position;
        }
    }

    /// <summary>
    /// Move to front of all windows.
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

    private RectTransform m_RectTransform;

    private RectTransform m_ParentOrigin;

    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_ParentOrigin = transform.parent.GetComponent<RectTransform>();
    }

    private DropSlotObject m_CurrentDropSlot;

    public void AttachToDropSlot(DropSlotObject slot)
    {
        m_CurrentDropSlot = slot ?? throw new System.ArgumentNullException();

        // we need to set parent
        transform.position = m_CurrentDropSlot.transform.position;
        transform.SetParent(m_CurrentDropSlot.transform, true);
        transform.SetAsLastSibling();

        m_ParentOrigin = slot.transform.GetComponent<RectTransform>();
    }

    private DropSlotObject FindNearestDropSlot(PointerEventData eventData)
    {
        List<RaycastResult> lst = new List<RaycastResult>();

        

        CanvasAccessor.Raycaster.Raycast(eventData, lst);

        // MH: hack. the first raycasted is always top one (draggable itself)
        // if second one is not dropslot we can say that there's no valid target to drop
        // DropSlot component should be on top of raycasted object.

        if (lst.Count < 2)
            return null;

        return lst[1].gameObject.GetComponentInParent<DropSlotObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
