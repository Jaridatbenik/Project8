using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour
{

    public GameObject correctObject;

    /// <summary>
    /// If left blank or = null it will not snap to a selected object but just to something, 
    /// I don't know its been 4 weeks since I looked at this shit
    /// </summary>
    public Transform snapToThis;
    /// <summary>
    /// If left blank or = null it will spawn the particles on the default location
    /// </summary>
    public Transform spawnParticlesHere;

    public GameObject canAttachParticle;

    [HideInInspector]
    public CircleHandler circleHand;
    [HideInInspector]
    public SnapperHandler hand;
    [HideInInspector]
    public Sequensing seq;
    [HideInInspector]
    public Collider col;

    [HideInInspector]
    public ObjectPicker pick;

    public List<Collider> ignoreThisIfNeedid = new List<Collider>();

    [HideInInspector]
    public Transform parentObject;

    public int neededSequence = 0;

    void Start()
    {
        circleHand = FindObjectOfType<CircleHandler>();
        hand = FindObjectOfType<SnapperHandler>();
        seq = FindObjectOfType<Sequensing>();
        pick = FindObjectOfType<ObjectPicker>();
        parentObject = transform.parent;

        for (int i = 0; i < ignoreThisIfNeedid.Count; i++)
        {
            Physics.IgnoreCollision(ignoreThisIfNeedid[i], GetComponent<BoxCollider>());
        }
    }

    void OnTriggerEnter(Collider col)
    {
        StaticSnapperHandler.Set(this);
        //if(col.gameObject.GetComponent<>)
        this.col = col;
        
    }

    void OnTriggerExit(Collider col)
    {
        StaticSnapperHandler.SendNonCompletion(this);
        this.col = null;
        //circleHand.CancelCircle();
    }


    public void StartBuilding()
    {
        circleHand.SpawnCircle(4f, CircleTypes.ObjectSelection, CheckIfCorrect, Canceled);
        hand.SpawnInBuildParticles(spawnParticlesHere == null ? transform.position : spawnParticlesHere.position);
    }

    public void WrongObject()
    {
        hand.SpawnWrongParticles(spawnParticlesHere == null ? transform.position : spawnParticlesHere.position);
        hand.DestroyParticle();
    }

    public void CheckIfCorrect()
    {
        StaticSnapperHandler.SendCompletion(this);

        /*
        if (col.gameObject != correctObject || neededSequence != seq.currentBodyPart)
        {
            hand.SpawnWrongParticles(transform.position);
            hand.DestroyParticle();
        }
        else
        {
            seq.UpCount();
            pick.ReleaseObject();
            hand.SpawnCorrectParticles(transform.position);
            col.transform.SetParent(snapToThis != null ? snapToThis : parentObject);
            for (int i = 0; i < pick.transform.childCount; i++)
            {
                if (pick.transform.GetChild(i).GetComponent<PickableObject>() != null)
                {
                    Destroy(pick.transform.GetChild(i).GetComponent<PickableObject>());
                }
            }
            col.transform.localPosition = Vector3.zero;
            col.transform.rotation = new Quaternion(0, 0, 0, 0);
            Destroy(col.transform.GetChild(0).gameObject.GetComponent<PickableObject>());
            Destroy(col.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(col.GetComponent<Rigidbody>());
            Destroy(col);
            Destroy(GetComponent<BoxCollider>());
            Destroy(this);
            hand.DestroyParticle();
        }
        */
        Debug.Log("Finished :" + this.name);
    }

    public void OnFinish()
    {
        seq.UpCount();
        pick.ReleaseObject();
        hand.SpawnCorrectParticles(spawnParticlesHere == null ? transform.position : spawnParticlesHere.position);
        col.transform.SetParent(snapToThis != null ? snapToThis : parentObject);
        for (int i = 0; i < pick.transform.childCount; i++)
        {
            if (pick.transform.GetChild(i).GetComponent<PickableObject>() != null)
            {
                Destroy(pick.transform.GetChild(i).GetComponent<PickableObject>());
            }
        }
        col.transform.localPosition = Vector3.zero;
        col.transform.rotation = new Quaternion(0, 0, 0, 0);
        Destroy(col.transform.GetChild(0).gameObject.GetComponent<PickableObject>());
        Destroy(col.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
        Destroy(col.GetComponent<Rigidbody>());
        Destroy(col);
        Destroy(GetComponent<BoxCollider>());
        hand.DestroyParticle();
        try
        {
            Destroy(canAttachParticle);
        }catch { }

        Destroy(this);
    }

    public void Canceled()
    {
        hand.DestroyParticle();
        hand.SpawnWrongParticles(spawnParticlesHere == null ? transform.position : spawnParticlesHere.position);
    }
}

public static class StaticSnapperHandler
{
    public static Snapper snap;


    public static void Set(Snapper snapsnap)
    {
        snap = snapsnap;
        snap.StartBuilding();
        Debug.Log(snap);
    }

    public static void SendCompletion(Snapper snapsnap)
    {
        if (snap == snapsnap && snap.col.gameObject == snap.correctObject && snap.neededSequence == snap.seq.currentBodyPart)
        {
            snap.OnFinish();
            Debug.Log("correct object");
        }
        else
        {
            snap.Canceled();
            Debug.Log("not the correct object");
        }
    }

    public static void SendNonCompletion(Snapper snapsnap)
    {
        try
        {
            if (snap == snapsnap)
            {
                snap.circleHand.CancelCircle();
                snap = null;
            }
            else
            {
                snap.Canceled();
                snap = null;
                Debug.Log("a non current snapper got disconected from somewhere, this is not a problem. dont worry, be happy");
            }
        }catch { }
    }
}