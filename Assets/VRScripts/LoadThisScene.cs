using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadThisScene : MonoBehaviour {

	public void LaadDezeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
