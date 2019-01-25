using UnityEngine;

//[RequireComponent(typeof(SteamVR_LaserPointer))]
public class ObjectPicker_Unlocks : MonoBehaviour
{
    public SteamVR_LaserPointer pointer;

    [HideInInspector]
    public GameObject currentSelected;
    [HideInInspector]
    public PickableObject currentTarget;
    [HideInInspector]
    public bool isOnObject = false;

    public Transform parenter;

    PickerType type = PickerType.PickableObject;
    LeverHandler lever;

    NumpadHandler padHandler;

    float cooldown = 0;

    void Start()
    {
        //pointer = GetComponent<SteamVR_LaserPointer>();
        pointer.PointerIn -= HandlePointerIn;
        pointer.PointerIn += HandlePointerIn;
        pointer.PointerOut -= HandlePointerOut;
        pointer.PointerOut += HandlePointerOut;

        padHandler = FindObjectOfType<NumpadHandler>();
    }

    void Update()
    {
        if (cooldown < 2)
        {
            cooldown++;
        }

        if (currentSelected != null)
        {
            if (isOnObject)
            {
                //currentSelected.gameObject.GetComponent<Renderer>().material.color = Color.green;

            }
            else
            {
                /*
                if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown())
                {
                    return;
                }
                */
            }
            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() ||
                SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown())
            {

                if (type != PickerType.Lever)
                {
                    try
                    {
                        currentSelected.GetComponent<NumPad_Button>().OnPressDown();
                    }
                    catch { }
                }
                else
                {
                    lever.SwitchLever();
                }
                return;
            }



            if ((SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerDown() ||
                SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerDown()) && cooldown > 1)
            {
                // PickupObject();


                //cooldown = 0;
                /*
                currentSelected.SetActive(false);
                currentTarget.referenceObject.SetActive(true);
                currentTarget.referenceObject.transform.SetParent(transform);
                currentTarget.referenceObject.transform.localPosition = new Vector3(0, 0, 0.2f);
                pointer.enabled = false;
                */
            }
            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetHairTriggerUp() ||
                SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetHairTriggerUp())
            {
                //ReleaseObject();                            
                try
                {
                    currentSelected.GetComponent<NumPad_Button>().OnPressUp();
                }
                catch { }

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

    //void PickupObject()
    //{
    //    try
    //    {
    //        currentSelected.transform.SetParent(parenter);
    //        currentSelected.transform.localPosition = new Vector3(0, 0, 0.2f) + currentSelected.GetComponent<PickableObject>().offset;
    //        pointer.enabled = false;
    //    }
    //    catch { }
    //}

    //public void ReleaseObject()
    //{
    //    try
    //    {
    //        currentSelected.transform.SetParent(FindObjectOfType<Snapper>().parentObject);
    //        pointer.enabled = true;
    //    }
    //    catch { }
    //}

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        //if (e.target.gameObject.GetComponent<PickableObject>() != null)
        //{
        //    currentSelected = e.target.GetComponent<PickableObject>().moveParent;
        //    currentTarget = e.target.gameObject.GetComponent<PickableObject>();
        //    isOnObject = true;
        //}
        if (e.target.gameObject.GetComponent<LeverHandler>() != null)
        {
            type = PickerType.Lever;
            isOnObject = true;
            lever = e.target.gameObject.GetComponent<LeverHandler>();
        }
        else
        {
            type = PickerType.PickableObject;
        }

        currentSelected = e.target.gameObject;
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        //isOnObject = false;
        if (currentSelected.GetComponent<NumPad_Button>())
            padHandler.ClearMat(currentSelected.GetComponent<MeshRenderer>());

        if (lever != null)
        {
            lever = null;
        }

        currentSelected = null;
    }
}
