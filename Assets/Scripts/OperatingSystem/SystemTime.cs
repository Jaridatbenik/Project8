using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class SystemTime : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI t;
    DateTime dt;

    void FixedUpdate()
    {
        dt = DateTime.Now;
        string dateText = dt.ToString("HH:mm dd/MM/yyyy");        

        t.text = dateText;
    }
}