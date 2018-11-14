using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    Camera mcamera;
    public GameObject changelocation;
    Vector3 mouseposition = new Vector3();

    void Start()
    {
        //mcamera = Camera.main;
    }

    private void Update()
    {
        mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mouseposition);
        if (Input.GetMouseButtonDown(0))
        {
            changelocation.transform.position = new Vector3(mouseposition.x, 0.50f, mouseposition.z);
        }
    }
}
