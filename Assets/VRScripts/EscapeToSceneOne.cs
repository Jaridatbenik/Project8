using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeToSceneOne : MonoBehaviour {

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //zo ga je weer terug naar de eerste scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(7);
        }
	}
}
