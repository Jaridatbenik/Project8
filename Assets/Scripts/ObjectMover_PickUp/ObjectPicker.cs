using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickerType
{
    PickableObject,
    Lever
}

//[RequireComponent(typeof(SteamVR_LaserPointer))]
public class ObjectPicker : MonoBehaviour
{
    public SteamVR_LaserPointer pointer;

    [HideInInspector]
    public GameObject currentSelected;
    [HideInInspector]
    public PickableObject currentTarget;
    [HideInInspector]
    public bool isOnObject = false;
    [HideInInspector]
    public LeverHandler lever;

    public Transform parenter;

    PickerType type = PickerType.PickableObject;

    float cooldown = 0;

    void Start()
    {
        //pointer = GetComponent<SteamVR_LaserPointer>();
        pointer.PointerIn -= HandlePointerIn;
        pointer.PointerIn += HandlePointerIn;
        pointer.PointerOut -= HandlePointerOut;
        pointer.PointerOut += HandlePointerOut;

        ReleaseObject();
    }

    void Update()
    {
        if(cooldown < 2)
        {
            cooldown++;
        }

        if(currentSelected != null)
        {
            if (isOnObject)
            {
                //currentSelected.gameObject.GetComponent<Renderer>().material.color = Color.green;

            }else
            {
                if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown())
                {
                    return;
                }
            }


            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() && cooldown > 1)
            {
                if (type == PickerType.PickableObject)
                {
                    PickupObject();
                }else if(type == PickerType.Lever)
                {
                    lever.SwitchLever();
                }
                cooldown = 0;
                /*
                currentSelected.SetActive(false);
                currentTarget.referenceObject.SetActive(true);
                currentTarget.referenceObject.transform.SetParent(transform);
                currentTarget.referenceObject.transform.localPosition = new Vector3(0, 0, 0.2f);
                pointer.enabled = false;
                */
            }
            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp())
            {
                ReleaseObject();
                /*
                currentSelected.SetActive(true);
                currentTarget.referenceObject.SetActive(false);
                currentTarget.referenceObject.transform.SetParent(null);
                currentTarget.referenceObject.transform.position = new Vector3(0, 0, 0);
                pointer.enabled = true;
                */
            }
        }
    }

    void PickupObject()
    {
        try
        {
            currentSelected.transform.SetParent(parenter);
            currentSelected.transform.localPosition = new Vector3(0, 0, 0.2f) + currentSelected.GetComponent<PickableObject>().offset;
            pointer.enabled = false;
        }
        catch { }
    }

    public void ReleaseObject()
    {
        try
        {
            currentSelected.transform.SetParent(null);
            //FindObjectOfType<Snapper>().parentObject

            pointer.enabled = true;
        }
        catch { }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.GetComponent<PickableObject>() != null)
        {
            //Debug.Log(e.distance);
            type = PickerType.PickableObject;
            currentSelected = e.target.GetComponent<PickableObject>().moveParent;
            currentTarget = e.target.gameObject.GetComponent<PickableObject>();
            isOnObject = true;
        }else if(e.target.gameObject.GetComponent<LeverHandler>() != null)
        {
            type = PickerType.Lever;
            isOnObject = true;
            lever = e.target.gameObject.GetComponent<LeverHandler>();
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        isOnObject = false;
    }
}
