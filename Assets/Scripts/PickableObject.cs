using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {

    public GameObject moveParent;

    public Vector3 offset;

    public Collider ignoreThisCollider;

    private void Start()
    {
        Physics.IgnoreCollision(ignoreThisCollider, GetComponent<BoxCollider>());
    }
}
