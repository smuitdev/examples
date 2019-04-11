using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Повторно используемый объект
/// </summary>
public class PoolObject : MonoBehaviour
{
    // уникальный ключ префаба
    [HideInInspector]
    [SerializeField] private int m_PrefabKey;

    /// <summary>
    /// свойство наружу
    /// </summary>
    public int prefabKey => m_PrefabKey;

    public const int InvalidPrefabKey = 0;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (PrefabUtility.GetPrefabAssetType(gameObject) == PrefabAssetType.Regular)
        {
            m_PrefabKey = AssetDatabase.GetAssetPath(gameObject).GetHashCode();
        }
    }
#endif
}
