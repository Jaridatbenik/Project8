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

    CircleHandler circleHand;
    SnapperHandler hand;
    Sequensing seq;

    Collider col;

    ObjectPicker pick;

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
        //if(col.gameObject.GetComponent<>)
        this.col = col;
        circleHand.SpawnCircle(4f, CircleTypes.ObjectSelection, CheckIfCorrect, Canceled);
        hand.SpawnInBuildParticles(transform.position);
    }

    void OnTriggerExit(Collider col)
    {
        this.col = null;
        circleHand.CancelCircle();
    }


    void CheckIfCorrect()
    {
        //try
        //{
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
        Debug.Log("Finished :" + this.name);
        // }catch
        //{
        //   Debug.Log("nothing");
        //}
    }

    void Canceled()
    {
        hand.DestroyParticle();
        hand.SpawnWrongParticles(transform.position);
    }
}
