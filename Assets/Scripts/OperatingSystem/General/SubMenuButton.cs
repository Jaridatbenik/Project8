using System.Collections.Generic;
using UnityEngine;

public class SubMenuButton : MonoBehaviour
{    
    RectTransform t;

    [Header("Example: 'Settings'. No 'SubMenu/' needed.")]      //The code will add the "SubMenu/" because submenu's should be in that folder.
    public string submenuName;
    SubMenu submenu;                                            //This will be the instantiated submenu.
    [HideInInspector]public Transform submenuCanvas;
    public bool isParent = false;                               //In case this class is part of a Quit-Button or something like that. (You can ignore it while by leaving it false).

    void Awake()
    {
        if (GameObject.Find("SubmenuCanvas") != null)
            submenuCanvas = GameObject.Find("SubmenuCanvas").transform;
        else
        {
            Debug.LogError("Can't find 'SubMenuCanvas'.");
        }
        
        t = GetComponent<RectTransform>();


        if (isParent)
        {
            if (transform.parent.GetComponent<SubMenu>() != null)
                submenu = transform.parent.GetComponent<SubMenu>();
            else if (transform.parent.transform.parent.GetComponent<SubMenu>() != null)
                submenu = transform.parent.transform.parent.GetComponent<SubMenu>();
            else submenu = null;
            return;
        }

        if (!GameObject.Find(submenuName) && !isParent)
        {
            try
            {
                SubMenu newMenu = Resources.Load<SubMenu>("SubMenus/" + submenuName);
                submenu = Instantiate(newMenu, submenuCanvas);
                submenu.transform.SetAsLastSibling();
            }
            catch
            {
                Debug.LogError("Either SubMenuName in '" + name +  "' SubMenuButton-script doesn't exist, or you forgot to add the SubMenu-script.");
            }
            

            submenu.name = submenuName;
            submenu.pressedButton = t;      
        }
        else
            submenu = GameObject.Find(submenuName).GetComponent<SubMenu>();       
        
        
        if(submenu != null && submenu.GetComponentsInChildren<SubMenuButton>().Length > 0)
        {
            List<SubMenuButton> buttons = new List<SubMenuButton>();
            for(int i = 0; i < submenu.GetComponentsInChildren<SubMenuButton>().Length; i++)
            {
                buttons.Add(submenu.GetComponentsInChildren<SubMenuButton>()[i]);

                //if (buttons[i].isParent)
                    //buttons[i].darkenedImage = darkenedImage;                
            }
        } 
    }


    public void SubMenuToggle()
    {        
        if (submenu != null)
        {
            submenu.gameObject.SetActive(true);
            submenu.transform.SetAsLastSibling();

            if (!isParent)
            {
                submenu.pressedButton = t;
                submenu.shrunkPoints.Clear();
                submenu.FillPointsList(submenu.shrunkPoints, t);
                submenu.shrunkPos = t.position;
            }
            
            submenu.Toggle();                                                                        
        }        
    }    
}