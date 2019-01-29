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
    public GameObject snapToThis;
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
    public GameObject parentObject;

    public int neededSequence = 0;

    void Start()
    {
        circleHand = FindObjectOfType<CircleHandler>();
        hand = FindObjectOfType<SnapperHandler>();
        seq = FindObjectOfType<Sequensing>();
        pick = FindObjectOfType<ObjectPicker>();
        parentObject = transform.parent.gameObject;

        for (int i = 0; i < ignoreThisIfNeedid.Count; i++)
        {
            try
            {
                Physics.IgnoreCollision(ignoreThisIfNeedid[i], GetComponent<BoxCollider>());
            }
            catch { }
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
        try
        {
            circleHand.SpawnCircle(4f, CircleTypes.ObjectSelection, CheckIfCorrect, Canceled);
            hand.SpawnInBuildParticles(spawnParticlesHere == null ? transform.position : spawnParticlesHere.position);
        }
        catch { }
    }

    public void WrongObject()
    {
        hand.SpawnWrongParticles(spawnParticlesHere == null ? transform.position : spawnParticlesHere.position);
        hand.DestroyParticle();
    }

    public void CheckIfCorrect()
    {
        StaticSnapperHandler.SendCompletion(this);


    }

    public void OnFinish()
    {
        Debug.Log("Finished :" + this.name);

        seq.UpCount();
        Destroy(col.transform.GetComponent<Rigidbody>());
        if(pick.currentSelected.gameObject == col.gameObject)
        {
            pick.currentSelected = null;
        }
        pick.ReleaseObject();
        hand.SpawnCorrectParticles(spawnParticlesHere == null ? transform.position : spawnParticlesHere.position);

        correctObject.transform.SetParent(snapToThis != null ? snapToThis.transform : parentObject.transform);

        
        for (int i = 0; i < pick.transform.childCount; i++)
        {
            if (pick.transform.GetChild(i).GetComponent<PickableObject>() != null)
            {
                Destroy(pick.transform.GetChild(i).GetComponent<PickableObject>());
            }
        }
        


        correctObject.transform.localPosition = Vector3.zero;
        correctObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        Destroy(correctObject.transform.GetChild(0).gameObject.GetComponent<PickableObject>());
        Destroy(correctObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
        Destroy(correctObject.GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider>());
        Destroy(correctObject.GetComponent<Collider>());

        hand.DestroyParticle();
        try
        {
            Destroy(canAttachParticle);
        }catch { }


        Destroy(this);
        return;
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