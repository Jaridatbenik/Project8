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
        mcamera = Camera.main;
    }

    private void Update()
    {
        Raycast();
    }

    public void Raycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mcamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 20))
            {
                changelocation.transform.position = hit.point;
            }
        }
    }
}
