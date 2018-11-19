using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour {
    public Text highest;
    public Text furthest;
    public Text score;

    public int honk = 0;

    float[] honkscore = {-20,-40,-60,-80};

    public bool ballhit = false;
    public bool ballground = false;

    public List<GameObject> ballposition;

    bool[] scoreCheck = new bool[4] {false,false,false,false};

    float scoreHighest = 0;
    float tempHeight = 0;

    float scoreFar = 0;
    float tempFar = 0;

    float timer = 0.1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ballhit == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                if (ballposition.Count > 0)
                {
                    tempHeight = ballposition[ballposition.Count - 1].gameObject.transform.position.y;

                    if (tempHeight > scoreHighest)
                    {
                        scoreHighest = tempHeight;
                        highest.text = Mathf.RoundToInt(scoreHighest).ToString();
                    }

                    tempFar = ballposition[ballposition.Count - 1].gameObject.transform.position.x;

                    if (tempFar < scoreFar)
                    {
                        scoreFar = tempFar;
                        furthest.text = Mathf.RoundToInt(scoreFar).ToString();
                    }
                }
                Score();
                CheckBall();
                timer = 0.2f;
            }
        }
    }
    void Score()
    {
        Debug.Log(honk);
        if(tempFar > honkscore[0] && scoreCheck[0] == false)
        {
            honk++;
            scoreCheck[0] = true;
        }
        if (tempFar > honkscore[1] && scoreCheck[1] == false)
        {
            honk++;
            scoreCheck[1] = true;
        }
        if (tempFar > honkscore[2] && scoreCheck[2] == false)
        {
            honk++;
            scoreCheck[2] = true;
        }
        if (tempFar > honkscore[3] && scoreCheck[0] == false)
        {
            honk++;
            scoreCheck[3] = true;
        }
    }
    void CheckBall()
    {
        //Debug.Log(55);
            if (tempHeight < 1)
            {
                ballposition[ballposition.Count - 1].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                ballhit = false;
                ballground = true;
                scoreCheck[0] = false;
                scoreCheck[1] = false;
                scoreCheck[2] = false;
                scoreCheck[3] = false;

        }
    }
}