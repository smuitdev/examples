using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{


    public static StartupController Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //
        transform.position = Vector3.zero;
    }

    private void OnBeforeTransformParentChanged()
    {
        
    }

    private void OnTransformParentChanged()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
