using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLaucher : MonoBehaviour {

    public GameObject ball;
    public float velocityBall;
    public float timer = 3;
    public int timerInt = 3;

    GameManger gameManger;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocityBall, 1, 0); 
    }

    // Use this for initialization
    void Start () {
        gameManger = GameObject.Find("BallLaucher").GetComponent<GameManger>();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (gameManger.ballhit == false && gameManger.ballground == false)
        {
            if (timer < 0)
            {
                Instantiate(ball, new Vector3(-20, 1.2f, 0), Quaternion.identity);
                timer = timerInt;
            }
        }
	}
}
