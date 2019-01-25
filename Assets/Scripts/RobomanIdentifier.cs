using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobomanIdentifier : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<LeverHandler>().robot = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
