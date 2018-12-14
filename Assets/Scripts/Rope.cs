using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    LineRenderer line;

    public GameObject ropePointA;
    public GameObject ropePointB;

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        line.SetPositions(new Vector3[2] { ropePointA.transform.position, ropePointB.transform.position });
	}
}
