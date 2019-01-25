using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDinghandler : MonoBehaviour {

	void Start () {
        FindObjectOfType<Sequensing>().inputDing = gameObject;
        gameObject.SetActive(false);
	}
}
