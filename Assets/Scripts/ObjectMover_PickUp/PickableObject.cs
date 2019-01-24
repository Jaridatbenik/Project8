using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {

    public GameObject moveParent;

    public Vector3 offset;

    public Collider ignoreThisCollider;
    public List<Collider> extraCollidersToIgnore = new List<Collider>();

    private void Start()
    {
        try
        {
            Physics.IgnoreCollision(ignoreThisCollider, GetComponent<BoxCollider>());
            for(int i =0; i < extraCollidersToIgnore.Count; i++)
            {
                Physics.IgnoreCollision(extraCollidersToIgnore[i], GetComponent<BoxCollider>());
            }
        }catch { }
    }
}
