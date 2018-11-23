using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongEnemy : MonoBehaviour
{
    PingPongHit pongHit;   
    RaycastHit hit;
    GameObject enemyTarget;
    public GameObject ball;

    Vector3 newPosition;
    GameObject playerTarget;

    public float enemySpeed = 3;
    private float speedBackup;
    private float maxSpeedChance = 1.01f;
    private float extraLookPosHeight = 1;

    public bool allowedToMove = true;
        
    
    void Start()
    {
        playerTarget = GameObject.Find("PlayerTarget");
        enemyTarget = GameObject.Find("EnemyTarget");
        AssignBall();
        speedBackup = enemySpeed;
    }

    void Update()
    {
        Debug.DrawLine(gameObject.transform.position, ball.transform.position, Color.red);

        int pNum = ball.GetComponent<PingPongHit>().posessionNumber; 
        Move();                     
    }


    public void RandomSpeedGenerator()
    {
        float speed = Random.Range(0, speedBackup * maxSpeedChance);
        enemySpeed = speed;

        Debug.Log(enemySpeed);
    }

    public void Move()
    {
        if(allowedToMove)
        {
            float newY = Mathf.Lerp(gameObject.transform.position.y, ball.transform.position.y, enemySpeed / 1.2f);
            float newZ = Mathf.Lerp(gameObject.transform.position.z, ball.transform.position.z, enemySpeed);
            gameObject.transform.position = new Vector3(transform.position.x, newY, newZ);
            Vector3 lookPos = new Vector3(playerTarget.transform.position.x, playerTarget.transform.position.y + extraLookPosHeight, playerTarget.transform.position.z);
        }        
    }

    public void AssignBall()
    {    
        ball = GameObject.FindGameObjectWithTag("PingPongBall");
        pongHit = ball.GetComponent<PingPongHit>();
    }
}