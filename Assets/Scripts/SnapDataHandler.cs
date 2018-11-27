using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapDataHandler : MonoBehaviour {

    [Tooltip("Put all the objects with the detectable colliders in here")]
    public List<GameObject> snapDetect;
    [Tooltip("These are the points the snappable objects should connect to")]
    public List<Transform> snappingPoints;
    [Tooltip("All the blacklist handlers")]
    public List<BlackListData> blackList;

    private void Start()
    {
        foreach(GameObject obj in snapDetect)
        {
            obj.AddComponent<DetectSnap>();
            obj.GetComponent<DetectSnap>().handler = this;
        }
    }


}


public class DetectSnap : MonoBehaviour
{
    public SnapDataHandler handler;

    private void OnTriggerStay(Collider other)
    {
        
    }
}

[System.Serializable]
public class BlackListData
{
    [SerializeField]
    [Tooltip("What snappable object is this list the blacklist for")]
    public GameObject isFor;
    [SerializeField]
    [Tooltip("Put all the blacklisted objects from one snappoint in here")]
    public List<GameObject> blacklisted;
}