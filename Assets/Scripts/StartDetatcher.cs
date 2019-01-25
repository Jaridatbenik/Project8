using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDetatcher : MonoBehaviour {



	void Start () {

        ResetRobot rob = FindObjectOfType<ResetRobot>();

        for(int i = 0; i < transform.parent.childCount; i++)
        {
            rob.robotOnderdelen.Add(transform.parent.GetChild(i).gameObject);
        }

        transform.parent.DetachChildren();
	}
}
