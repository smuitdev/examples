using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public GameObject other;

    [SerializeField] private GameObject m_ObjectA;

    public GameObject objectA => m_ObjectA;

    // Start is called before the first frame update
    void Start()
    {
        other.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("1");
    }
}
