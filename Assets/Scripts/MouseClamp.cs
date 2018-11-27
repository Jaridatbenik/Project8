using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClamp : MonoBehaviour
{

    void Start()
    {
        print(Screen.width + "/" + Screen.height);    
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}