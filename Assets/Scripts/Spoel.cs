using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spoel : MonoBehaviour {

    [HideInInspector] public LineRenderer line;
    [HideInInspector] public KabelData kabelData;

    public GameObject kabelPrefab;

    public GameObject CreateKabel(SpoelHandler start)
    {
        GameObject obj = Instantiate(kabelPrefab);
        line = obj.GetComponent<LineRenderer>();
        kabelData = obj.GetComponent<KabelData>();
        kabelData.attachedSpoel = this.gameObject;
        kabelData.startPoint = start;
        kabelData.kleur = start.kleurtje;
        return obj;
    }

    public void AttachKabelToPoint(SpoelHandler eind)
    {
        try
        {
            kabelData.endPoint = eind;
            kabelData.attachedSpoel = null;
        }
        catch { }

    }
}
