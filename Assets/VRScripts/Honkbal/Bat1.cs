using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat1 : MonoBehaviour {
    public GameObject hand;
    public float velocityYMulti = 4.5f, velocityXZMulti = 3;

    GameManger gameManger;

    Rigidbody rigidbodyBall;
    
    private void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.name == "Honkball(Clone)")
        {
            gameManger.ballhit = true;
            gameManger.ballground = false;
            rigidbodyBall = collision.gameObject.GetComponent<Rigidbody>();
            gameManger.ballposition.Add(collision.gameObject);
            rigidbodyBall.constraints = RigidbodyConstraints.None;
            rigidbodyBall.velocity = /* SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).velocity;*/ new Vector3(-1, 1, 0);
            rigidbodyBall.velocity = new Vector3(rigidbodyBall.velocity.x * velocityXZMulti, rigidbodyBall.velocity.y * velocityYMulti, rigidbodyBall.velocity.z * velocityXZMulti);
          //  gameManger.ballhit = true;

        }
      // so dat die niet weg leid als je hem laat vallen  
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        }
    
        void Start()
        {
        gameManger = GameObject.Find("BallLaucher").GetComponent<GameManger>();
        }

    }