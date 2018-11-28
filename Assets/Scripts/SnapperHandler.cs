using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapperHandler : MonoBehaviour {

    public GameObject wrongParticles;
    public GameObject correctParticles;

    public void SpawnWrongParticles(Vector3 worldPos)
    {
        Destroy(Instantiate(wrongParticles, worldPos, Quaternion.identity), 4f);
    }

    public void SpawnCorrectParticles(Vector3 worldPos)
    {
        Destroy(Instantiate(correctParticles, worldPos, Quaternion.identity), 4f);
    }
}
