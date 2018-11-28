using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapper : MonoBehaviour {

    public GameObject correctObject;
    CircleHandler circleHand;
    SnapperHandler hand;

    Collider col;

    void Start()
    {
        circleHand = FindObjectOfType<CircleHandler>();
        hand = FindObjectOfType<SnapperHandler>();
    }

    void OnTriggerEnter(Collider col)
    {
        this.col = col;
        circleHand.SpawnCircle(4f, CircleTypes.ObjectSelection, CheckIfCorrect, Canceled);
    }

    void OnTriggerExit(Collider col)
    {
        this.col = null;
        circleHand.CancelCircle();
    }
    

    void CheckIfCorrect()
    {
        try
        {
            if (col.gameObject != correctObject)
            {
                hand.SpawnWrongParticles(transform.position);
            }
            else
            {
                hand.SpawnCorrectParticles(transform.position);
                col.transform.localPosition = Vector3.zero;
                Destroy(col.GetComponent<Rigidbody>());
                Destroy(col);
                Destroy(GetComponent<BoxCollider>());
                Destroy(this);
            }
            Debug.Log("Finished");
        }catch
        {
            Debug.Log("nothing");
        }
    }

    void Canceled()
    {
        hand.SpawnWrongParticles(transform.position);
    }
}
