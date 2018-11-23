using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBallSpawn : MonoBehaviour
{
    GameObject bBallSpawn;
    Vector3 spawnpos;
    public ulong index;

    public GameObject bBall;
    public int ballLimit = 5;

    public List<GameObject> bBalls = new List<GameObject>();

    void Start()
    {
        bBallSpawn = GameObject.Find("Basketball");
        spawnpos = bBallSpawn.transform.position;
        bBalls.Add(bBall);
    }


    void Update()
    {
        if(bBalls.Count < 5)
        {
            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                GameObject ball = bBall;
                Instantiate(ball, spawnpos, Quaternion.identity);
                bBalls.Add(ball.gameObject);
            }
            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                GameObject ball = bBall;
                Instantiate(bBall, spawnpos, Quaternion.identity);
                bBalls.Add(ball);
            }
        }
    }
}