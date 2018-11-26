using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DialogReader.InitScenario("MetalMilitia");
            DialogueBuffer.PushFirst();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DialogReader.InitScenario("Scenario5");
            DialogueBuffer.PushFirst();
        }
    }
}
