using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGravity : MonoBehaviour {

	void Update () {
		if(gameObject.tag == "Picked")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
	}
}
