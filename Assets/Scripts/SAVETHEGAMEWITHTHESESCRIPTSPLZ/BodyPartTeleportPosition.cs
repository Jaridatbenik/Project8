using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartTeleportPosition : MonoBehaviour {

    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public Quaternion startRot;

    public GameObject attachTo;

	void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }
}
