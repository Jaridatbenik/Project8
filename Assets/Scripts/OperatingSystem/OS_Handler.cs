using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OS_Handler : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }
 
    void Update()
    {

    }
}