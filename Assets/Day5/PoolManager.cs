using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null )
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        m_PooledObjects = new Dictionary<int, Stack<GameObject>>();
    }

    // пул объектов которые будут повторно использоваться
    private Dictionary<int, Stack<GameObject>> m_PooledObjects;

    // выделение геймобъекта из пула
    public GameObject Spawn(GameObject sourcePrefab)
    {
        PoolObject p = sourcePrefab.GetComponent<PoolObject>();

        if (p == null)
            return null;

        if (p.prefabKey == PoolObject.InvalidPrefabKey)
            return null;

        Stack<GameObject> objectStack = null;

        // получение валидного пула объектов
        if(m_PooledObjects.TryGetValue(p.prefabKey, out objectStack))
        {
            
        }
        else
        {
            objectStack = new Stack<GameObject>();
            m_PooledObjects[p.prefabKey] = objectStack;
        }

        GameObject pooledObject = null;

        // objectStack точно не null
        if(objectStack.Count == 0)
        {
            pooledObject = Instantiate(sourcePrefab);
        }
        else
        {
            pooledObject = objectStack.Pop();
            pooledObject.SetActive(true);
        }

        pooledObject.BroadcastMessage(MsgOnSpawn, SendMessageOptions.DontRequireReceiver);

        pooledObject.transform.parent = null;

        return pooledObject;
    }

    // отправить на повтороное использование 
    public void Unspawn(GameObject pooledObject)
    {
        PoolObject p = pooledObject.GetComponent<PoolObject>();

        if (p == null)
            return;

        if (p.prefabKey == PoolObject.InvalidPrefabKey)
            return;

        pooledObject.BroadcastMessage(MsgOnUnspawn, SendMessageOptions.DontRequireReceiver);

        pooledObject.SetActive(false);

        m_PooledObjects[p.prefabKey].Push(pooledObject);

        pooledObject.transform.parent = transform;
    }


    public static readonly string MsgOnSpawn = "OnPoolSpawn";
    public static readonly string MsgOnUnspawn = "OnPoolUnspawn";
}
