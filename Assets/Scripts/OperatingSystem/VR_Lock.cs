using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Lock : MonoBehaviour
{
    GameObject cameraRig;

    void Awake()
    {
        cameraRig = GameObject.Find("[CameraRig]");
        ChangeLockState(false);
    }

    public void ChangeLockState(bool b)
    {
        cameraRig.SetActive(b);
    }
}