using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    Camera mcamera;
    public GameObject changelocation;
    Vector3 mouseposition = new Vector3();

    CircleHandler circleHandler;

    bool canTeleport = true;

    void Start()
    {
        mcamera = Camera.main;
        circleHandler = GameObject.FindObjectOfType<CircleHandler>();
    }

    private void Update()
    {
        if (canTeleport)
        {
            Raycast();
        }
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
                canTeleport = false;
                circleHandler.SpawnCircle(1.8f, CircleTypes.Teleportation, SetCanTeleportAgain, SetCanTeleportAgain);
            }
        }
    }

    void SetCanTeleportAgain()
    {
        canTeleport = true;
    }
}
