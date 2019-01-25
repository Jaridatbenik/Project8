using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRobot : MonoBehaviour {

    public GameObject oldRobot;
    public List<GameObject> robotOnderdelen = new List<GameObject>();
    
	void Start () {
        SpawnObjects();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CallResetRobot();
        }
	}

    void SpawnObjects()
    {

        robotOnderdelen.Add(Instantiate(oldRobot));
    }

    public void CallResetRobot()
    {
        for(int i = 0; i < robotOnderdelen.Count; i++)
        {
            Destroy(robotOnderdelen[i]);
        }
        robotOnderdelen.Clear();
        FindObjectOfType<Sequensing>().currentBodyPart = 0;
        
        SpawnObjects();
    }
}
