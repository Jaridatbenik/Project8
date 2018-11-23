using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honk : MonoBehaviour {
    public GameObject walker;
    public int honkNumber;
    int honkA;
    GameManger gameManger;
    bool newWalker = true;
	// Use this for initialization
	void Start () {
        gameManger = GameObject.Find("ballLaucher").GetComponent<GameManger>();
		
	}
	
	// Update is called once per frame
	void Update () {
        honkA = gameManger.honk;
        if (honkNumber == 1)
        {
            if(gameManger.ballground == true && newWalker == true)
            {
                Instantiate(walker, gameObject.transform.position, Quaternion.identity);
                newWalker = false;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == walker)
        {
            if(honkNumber == 1 && other.gameObject.GetComponent<Walkers>().currentHonk == -1 )
            {
                other.gameObject.GetComponent<Walkers>().currentHonk = honkA - 1 ;
                other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 5);
            }
        }
    }
}

