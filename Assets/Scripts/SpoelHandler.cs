using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoelHandler : MonoBehaviour
{
    //[HideInInspector]
    public KabelData data;
    public Transform customTransform;
    public KabelKleur kleurtje = KabelKleur.Rood;
    public bool canBeAStartingPoint = true;
    public bool canBeEndPoint = true;

    public KabelKleur isColor;
    public bool hasCableAttached = false;

    void Update()
    {

        if(data == null)
        {
            hasCableAttached = false;
        }
        try
        {
            if (canBeAStartingPoint)
            {
                GetComponent<Renderer>().material.color = KabelData.kabelKleurtjes[kleurtje];
            }else
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
        catch { }
    }

    public void DetatchKabel()
    {
        Debug.Log("Should make Data null for: " + this);
        this.data = null;
    }

    void OnTriggerEnter(Collider col)
    {
        if (data == null && col.GetComponent<Spoel>() != null)
        {
            Debug.Log("Ik raak een leeg block");
            if (col.GetComponent<Spoel>().kabelData == null && canBeAStartingPoint)
            {
                Debug.Log("Kabel Gestart");
                col.GetComponent<Spoel>().CreateKabel(this);
                data = col.GetComponent<Spoel>().kabelData;
            }
            else
            {
                if (canBeEndPoint)
                {
                    try
                    {
                        if (!hasCableAttached)
                        {
                            Debug.Log("Kabel Gestopt");
                            col.GetComponent<Spoel>().AttachKabelToPoint(this);
                            //data = null;
                            data = col.GetComponent<Spoel>().kabelData;
                            isColor = data.kleur;
                            hasCableAttached = true;
                            col.GetComponent<Spoel>().kabelData = null;
                            col.GetComponent<Spoel>().line = null;
                        }
                    }
                    catch { }

                }
            }
        }
        else
        {
            try
            {
                if (col.GetComponent<Spoel>().kabelData != null)
                {
                    Debug.Log("Ik trigger");
                    if (col.GetComponent<Spoel>().kabelData.startPoint.gameObject == gameObject && col.GetComponent<Spoel>().kabelData.endPoint == null)
                    {
                        Debug.Log("Ik trigger ook");
                        Debug.Log(col.GetComponent<Spoel>().kabelData.startPoint.gameObject + " : " + gameObject);
                        Destroy(data.gameObject);
                        hasCableAttached = false;
                    }
                    Debug.Log("End: " + col.GetComponent<Spoel>().kabelData.endPoint + " : Start: " + col.GetComponent<Spoel>().kabelData.startPoint);
                    if (data.endPoint != null && data.startPoint != null)
                    {
                        if (canBeEndPoint)
                        {
                            Debug.Log("nu moet hij detatchen");
                            data.RemoveLine();
                            col.GetComponent<Spoel>().AttachKabelToPoint(this);
                            data = col.GetComponent<Spoel>().kabelData;
                            col.GetComponent<Spoel>().kabelData = null;
                            col.GetComponent<Spoel>().line = null;
                        }
                    }
                }
                else
                {
                    if (canBeAStartingPoint)
                    {
                        data.RemoveLine();

                        col.GetComponent<Spoel>().CreateKabel(this);
                        data = col.GetComponent<Spoel>().kabelData;
                        hasCableAttached = false;
                        Debug.Log("Wants to start but a cable is already here");
                    }
                }
            }
            catch { }
        }
    }

    void OnTriggerExit(Collider col)
    {
       
    }
}
