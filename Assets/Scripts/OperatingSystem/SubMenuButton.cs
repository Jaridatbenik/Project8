using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SubMenuButton : MonoBehaviour
{
    
    RectTransform t;

    [Header("Example: 'Settings'. No 'SubMenu/' needed.")]
    public string submenuName;
    SubMenu submenu;
    [HideInInspector]public Transform submenuCanvas;
    public bool isParent = false;

    //[Header("Darkened image. Use these variables for exceptions (like menu's inside menu's)")]
    //public Image darkenedImage;     //Assign in Editor.    
    //[Header("If true, the spawned Menu's Quit-Button will call the same darkenedImage-Toggle.")]
    //public bool setSpawnedMenuQuitButtonImageToMine = false;        
    

    void Awake()
    {
        if (GameObject.Find("SubmenuCanvas") != null)
            submenuCanvas = GameObject.Find("SubmenuCanvas").transform;
        else
        {
            Debug.LogError("Can't find 'SubMenuCanvas'.");
        }

        //if(!assignImageManually)
            //darkenedImage = submenuCanvas.GetChild(1).GetComponent<Image>();
        
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
            }
            catch
            {
                Debug.LogError("Either SubMenuName in SubMenuButton-script doesn't exist, or you forgot to add the SubMenu-script.");
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