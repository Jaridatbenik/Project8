using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{
    [HideInInspector]
    public bool lookingAtObject;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    float x = Screen.width / 2;
    float y = Screen.height / 2;


    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f, 0.922f, 0.016f, 1f));


        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "InteractObject")
            {
                lookingAtObject = true;
                GameObject objectHit = hit.collider.GetComponent<GameObject>();
                if(Input.GetKey(KeyCode.Space))
                {
                    Debug.Log("investigate!");
                }
            }
            else
            {
                lookingAtObject = false;
            }
        }
    }

}
