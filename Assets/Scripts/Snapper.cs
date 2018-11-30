using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour {

    public GameObject correctObject;
    CircleHandler circleHand;
    SnapperHandler hand;
    Sequensing seq;

    Collider col;

    ObjectPicker pick;

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
                col.transform.SetParent(parentObject);
                for(int i = 0; i < pick.transform.childCount; i++)
                {
                    if(pick.transform.GetChild(i).GetComponent<PickableObject>() != null)
                    {
                        Destroy(pick.transform.GetChild(i).GetComponent<PickableObject>());
                    }
                }
                col.transform.localPosition = Vector3.zero;
                col.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(col.GetComponent<Rigidbody>());
                Destroy(col);
                Destroy(GetComponent<BoxCollider>());
                Destroy(this);
                hand.DestroyParticle();
            }
            Debug.Log("Finished");
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
