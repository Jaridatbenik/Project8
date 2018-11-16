using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CubeInfo;

public class TeleportHandler1 : MonoBehaviour
{
    Camera mcamera;
    public GameObject changelocation;
    Vector3 mouseposition = new Vector3();
    public Text cubeText;

    CubeInfomation cubeInfo = new CubeInfomation();

    CircleHandler circleHandler;

    bool canTeleport = true;

    void Start()
    {
        mcamera = Camera.main;
        circleHandler = GameObject.FindObjectOfType<CircleHandler>();
    }

    private void Update()
    {
        //if (canTeleport)
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
                if(hit.transform.tag == "CubeInfo")
                {
                    cubeText.text = cubeInfo.StringInfo + "\n" + cubeInfo.FloatInfo.ToString();
                    cubeText.rectTransform.position = Camera.main.WorldToScreenPoint(hit.transform.position + new Vector3(0, 50));
                }
                else
                {
                    changelocation.transform.position = hit.point;
                    canTeleport = false;
                    circleHandler.SpawnCircle(1.8f, CircleTypes.Teleportation, SetCanTeleportAgain, SetCanTeleportAgain);
                }
            }
        }
    }

    void SetCanTeleportAgain()
    {
        canTeleport = true;
    }
}
