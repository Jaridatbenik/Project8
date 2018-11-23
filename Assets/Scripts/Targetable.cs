using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour {

    public string objectName = "";
    [TextArea]
    public string explenationText = "";
    public GameObject referenceObject;
    public GameObject outlineObject;
}
