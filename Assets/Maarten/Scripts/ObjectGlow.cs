using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlow : MonoBehaviour {

    Light light;

    // Use this for initialization
    void Start ()
    {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if ((GameObject.Find("Main Camera").GetComponent<LookScript>().lookingAtObject) == true)
        {
            light.intensity = 1.0f;
        }
        else
        {
            light.intensity = 0;
        }
	}
}
