using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float timer;
    public Transform tempTrans;
    Vector3 holdVector;

    private void Start()
    {
        holdVector = gameObject.transform.localScale;
    }

    public void GoToObject(GameObject other)
    {
        transform.position = other.transform.position;
        transform.parent = other.transform;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent = tempTrans;
        gameObject.transform.localScale = holdVector;
    }
}