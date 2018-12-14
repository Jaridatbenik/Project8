using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMenus : MonoBehaviour
{
    [SerializeField]List<SubMenu> menus = new List<SubMenu>();  //temp serialized.

    public void ClearAllMenus()
    {        
        if(menus.Count < FindObjectsOfType<SubMenu>().Length)
        {
            GetAllMenus();            
        }
        for (int i = 0; i < menus.Count; i++)
        {            
            if (menus[i].transform.localScale.x > 0.2f)
            {
                menus[i].Toggle();
            }
        }        
    }

    void Start()
    {
        GetAllMenus();    
    }

    void GetAllMenus()
    {
        for(int i = 0; i < FindObjectsOfType<SubMenu>().Length; i++)
        {
            SubMenu current = FindObjectsOfType<SubMenu>()[i];
            menus.Add(current);
        }
    }
}