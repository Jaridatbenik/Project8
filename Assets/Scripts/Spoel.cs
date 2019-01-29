using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spoel : MonoBehaviour {

    [HideInInspector] public LineRenderer line;
    [HideInInspector] public KabelData kabelData;

    public GameObject kabelPrefab;

    public List<Collider> ignoreCollisionWithThese = new List<Collider>();

    private void Start()
    {
        IgnoreTheCollision();
    }

    public void IgnoreTheCollision()
    {
        Collider[] cols = transform.GetChild(1).GetComponents<Collider>();

        for (int i = 0; i < cols.Length; i++)
        {
            for(int f = 0; f < ignoreCollisionWithThese.Count; f++)
            {
                Physics.IgnoreCollision(cols[i], ignoreCollisionWithThese[f]);
            }
        }
    }

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
