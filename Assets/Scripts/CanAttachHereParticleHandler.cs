using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttachHereParticleHandler : MonoBehaviour {

    GameObject child;

    void Start()
    {
        child = transform.GetChild(0).gameObject;
        DisableMe();
    }

	public void EnableMe()
    {
        child.SetActive(true);
    }

    public void DisableMe()
    {
        child.SetActive(false);
    }
}
