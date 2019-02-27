using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAccessor : MonoBehaviour
{
    public static CanvasAccessor Instance
    {
        get
        {
            //



            return m_Instance;
        }

        set
        {
            //

            m_Instance = value;

            //
        }
    }

    private static CanvasAccessor m_Instance;

    public static CanvasAccessor GetAccessor()
    {
        return m_Instance;
    }

    private static void SetAccessor(CanvasAccessor newValue)
    {
        m_Instance = newValue;
    }

    /// <summary>
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </summary>

    public static GraphicRaycaster Raycaster { get; private set; }

    private void Start()
    {
        

        Raycaster = GetComponent<GraphicRaycaster>();

        if(Instance != null)
        {
            DestroyImmediate(gameObject);
            Debug.LogWarning("singleton");
            return;
        }

        Instance = this;
    }
}
