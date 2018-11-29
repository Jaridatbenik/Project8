using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapperHandler : MonoBehaviour {

    public GameObject wrongParticles;
    public GameObject correctParticles;
    public GameObject inBuildParticles;

    GameObject spawnedParticle;

    public void SpawnWrongParticles(Vector3 worldPos)
    {
        Destroy(Instantiate(wrongParticles, worldPos, Quaternion.identity), 4f);
    }

    public void SpawnCorrectParticles(Vector3 worldPos)
    {
        Destroy(Instantiate(correctParticles, worldPos, Quaternion.identity), 4f);
    }

    public void SpawnInBuildParticles(Vector3 worldPos)
    {
        spawnedParticle = Instantiate(inBuildParticles, worldPos, Quaternion.identity);
            Destroy(spawnedParticle, 4f);
    }

    public void DestroyParticle()
    {
        Destroy(spawnedParticle);
    }
}
