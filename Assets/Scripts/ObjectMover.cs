using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    Renderer rend;
    public Material finalMaterial;
    public Material previousMaterial;
    public float timer;
    public Transform tempTrans;
    Vector3 holdVector;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        holdVector = gameObject.transform.localScale;
    }

    public void GoToObject(GameObject other)
    {
        rend.sharedMaterial = finalMaterial;
        transform.position = other.transform.position;
        transform.parent = other.transform;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent = tempTrans;
        gameObject.transform.localScale = holdVector;
    }
}