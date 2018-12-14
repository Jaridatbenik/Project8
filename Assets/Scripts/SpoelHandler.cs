using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoelHandler : MonoBehaviour
{
    //[HideInInspector]
    public KabelData data;
    public Transform customTransform;
    public KabelKleur kleurtje = KabelKleur.Rood;
    
    public void DetatchKabel()
    {
        Debug.Log("Should make Data null for: " + this);
        this.data = null;
    }

    void OnTriggerEnter(Collider col)
    {
        if(data == null && col.GetComponent<Spoel>() != null)
        {
            Debug.Log("Ik raak een leeg block");
            if (col.GetComponent<Spoel>().kabelData == null)
            {
                Debug.Log("Kabel Gestart");
                col.GetComponent<Spoel>().CreateKabel(this);
                data = col.GetComponent<Spoel>().kabelData;
            }else
            {
                Debug.Log("Kabel Gestopt");
                col.GetComponent<Spoel>().AttachKabelToPoint(this);
                //data = null;
                data = col.GetComponent<Spoel>().kabelData;
                col.GetComponent<Spoel>().kabelData = null;
                col.GetComponent<Spoel>().line = null;
            }
        }else
        {
            if (col.GetComponent<Spoel>().kabelData != null)
            {
                Debug.Log("Ik trigger");
                if (col.GetComponent<Spoel>().kabelData.startPoint.gameObject == gameObject && col.GetComponent<Spoel>().kabelData.endPoint == null)
                {
                    Debug.Log("Ik trigger ook");
                    Debug.Log(col.GetComponent<Spoel>().kabelData.startPoint.gameObject + " : " + gameObject);
                    Destroy(data.gameObject);
                }
                if (col.GetComponent<Spoel>().kabelData.endPoint != null && col.GetComponent<Spoel>().kabelData.startPoint != null)
                {
                    Debug.Log("nu moet hij detatchen");
                }
            }else
            {
                data.RemoveLine();
                data.endPoint.DetatchKabel();
                data.startPoint.DetatchKabel();


                col.GetComponent<Spoel>().CreateKabel(this);
                data = col.GetComponent<Spoel>().kabelData;

                Debug.Log("Wants to start but a cable is already here");
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
       
    }
}
