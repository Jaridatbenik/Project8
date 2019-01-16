using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KabelKleur
{
    Rood,
    Groen,
    Blauw
}

public class KabelData : MonoBehaviour {

    //[HideInInspector]
    public SpoelHandler startPoint;
    //[HideInInspector]
    public SpoelHandler endPoint;
    //[HideInInspector]
    public KabelKleur kleur = KabelKleur.Rood;
    [HideInInspector]public GameObject attachedSpoel;
    bool inRemoveMode = false;
    float removeModeCounter = 0;

    LineRenderer line;

    public static Dictionary<KabelKleur, Color> kabelKleurtjes = new Dictionary<KabelKleur, Color>() { { KabelKleur.Rood, Color.red }, { KabelKleur.Groen, Color.green }, { KabelKleur.Blauw, Color.blue } };

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void RemoveLine()
    {
        if (!inRemoveMode)
        {
            GameObject startObj = new GameObject("KabelStart");
            GameObject endObj = new GameObject("KabelEnd");
            SpoelHandler newStartSpoel = startObj.AddComponent<SpoelHandler>();
            SpoelHandler newEndSpoel = endObj.AddComponent<SpoelHandler>();
            newStartSpoel.transform.position = startPoint.transform.position;
            newEndSpoel.transform.position = endPoint.transform.position;
            startPoint.DetatchKabel();
            endPoint.DetatchKabel();
            startPoint = newStartSpoel;
            endPoint = newEndSpoel;
            inRemoveMode = true;
        }
    }

    void Update()
    {
        if (inRemoveMode)
        {
            removeModeCounter += Time.deltaTime;

            if(removeModeCounter <= 3)
            {
                startPoint.transform.position -= new Vector3(0, 1f * Random.Range(0.8f, 6f) * Time.deltaTime, 0);
                endPoint.transform.position -= new Vector3(0, 1f * Random.Range(0.8f, 6f) * Time.deltaTime, 0);
                line.SetPositions(
                    new Vector3[2] {
                startPoint.customTransform != null ?
                startPoint.customTransform.position :
                startPoint.transform.position,
                endPoint.customTransform != null ?
                endPoint.customTransform.position :
                endPoint.transform.position
                    });
            }
            else
            {
                Destroy(startPoint.gameObject);
                Destroy(endPoint.gameObject);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (attachedSpoel != null)
            {
                if (startPoint != null)
                {
                    line.startColor = kabelKleurtjes[kleur];
                    line.endColor = kabelKleurtjes[kleur];
                    line.SetPositions(new Vector3[2] { startPoint.customTransform != null ? startPoint.customTransform.position : startPoint.transform.position, attachedSpoel.transform.position });
                }
            }
            else
            {
                line.startColor = kabelKleurtjes[kleur];
                line.endColor = kabelKleurtjes[kleur];
                line.SetPositions(
                    new Vector3[2] {
                startPoint.customTransform != null ?
                startPoint.customTransform.position :
                startPoint.transform.position,
                endPoint.customTransform != null ?
                endPoint.customTransform.position :
                endPoint.transform.position
                    });
            }
        }
    }
}
