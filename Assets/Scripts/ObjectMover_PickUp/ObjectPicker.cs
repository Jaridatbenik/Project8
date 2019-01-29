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
        if (Input.GetKeyDown(KeyCode.K))
        {

            foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
            {
                attachObj.EnableMe();
            }
        }

        if (Input.GetKeyUp(KeyCode.K))
        {

            foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
            {
                attachObj.DisableMe();
            }
        }

        try
        {
            if (cooldown < 2)
            {
                cooldown++;
            }


            if (currentSelected != null)
            {
                if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() ||
                    SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown())
                {

                    foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                    {
                        attachObj.EnableMe();
                    }
                }
                else if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp() ||
                    SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
                {
                    foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                    {
                        attachObj.DisableMe();
                    }
                }
                if (isOnObject)
                {

                }
                else
                {
                    if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() ||
                        SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown())
                    {

                        foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                        {
                            attachObj.EnableMe();
                        }
                        return;
                    }
                    else if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp() ||
                        SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
                    {
                        foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                        {
                            attachObj.DisableMe();
                        }
                        return;
                    }
                }


                if ((SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() ||
                    SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown()))
                {
                    if (type == PickerType.PickableObject)
                    {
                        PickupObject();
                    }
                    else if (type == PickerType.Lever)
                    {

                        lever.SwitchLever();
                    }
                    cooldown = 0;
                }
                if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp() ||
                    SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
                {
                    ReleaseObject();
                }
            }
            else
            {
                if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() ||
                    SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown())
                {
                    foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                    {
                        attachObj.EnableMe();
                    }
                }
                else if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp() ||
                    SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
                {
                    foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                    {
                        attachObj.DisableMe();
                    }
                }
                if (isOnObject)
                {

                }
                else
                {
                    if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() ||
                        SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown())
                    {

                        foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                        {
                            attachObj.EnableMe();
                        }
                        return;
                    }
                    else if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp() ||
                        SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
                    {
                        foreach (CanAttachHereParticleHandler attachObj in FindObjectsOfType<CanAttachHereParticleHandler>())
                        {
                            attachObj.DisableMe();
                        }
                        return;
                    }
                }

            }
        }
        catch { }
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
            //currentSelected = null;
            pointer.enabled = true;
        }
        catch {  }

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
        }
        if (e.target.gameObject.GetComponent<LeverHandler>() != null)
        {
            type = PickerType.Lever;
            isOnObject = true;
            lever = e.target.gameObject.GetComponent<LeverHandler>();
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        isOnObject = false;
        currentSelected = null;
    }
}
