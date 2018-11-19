using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PingPongHit : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;

    //Pak de playertargets om deze later te verplaatsen.
    GameObject playerTarget;
    GameObject enemyTarget;
    GameObject smashTarget;
    Vector3 startPos;
    
    GrabPongBat grabPongBat;
    BallRespawn ballRespawn;
    PongScore pongscore;
    PongEnemy pongEnemy;
    
    public float hitPower = 0.85f;
    public float lobHitPower = 0.50f;
    public float upMultiplier = 1.15f;
    public float smashPowerMultiplier = 3;
    public float smashUpPower = 2;
    public bool playerHit = false;
    public bool enemyHit = false;
    bool ballBanned = false;
    bool lobActive = false;

    bool playerCharging = false;
    bool playerSmashing = false;
    bool lobShot = false;
    public float lobShotChance = 50;
    private float hitrange = 0.55f;

    //0 = bal is los, 1 = bal is door jou geraakt, 2 = door de enemy geraakt.
    public int posessionNumber = 0;

    int rallyScore;

    Vector2 tPadInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballRespawn = GetComponent<BallRespawn>();

        playerTarget = GameObject.Find("PlayerTarget");
        enemyTarget = GameObject.Find("EnemyTarget");
        smashTarget = GameObject.Find("SmashTarget");
        
        grabPongBat = GameObject.Find("GetBat-Col").GetComponent<GrabPongBat>();
        pongscore = GameObject.Find("MainGameLogic").GetComponent<PongScore>();
        pongEnemy = GameObject.Find("EnemyBat").GetComponent<PongEnemy>();

        startPos = gameObject.transform.position;
    }

    void Update()
    {
        //velocity = rb.velocity;
        PlayerHitBall();
        PlayerInput();

        //Als de ball banned is, zal deze vanishen.
        if (ballBanned)
        {
            ballRespawn.vanishing = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Als de bal(het gameObject) "Model" raakt, 'IS' de bal van de speler, anders van de enemy.   
        if (other.gameObject.name == "Model" && playerCharging)
        {           
            pongEnemy.RandomSpeedGenerator();

            playerHit = true;
            enemyHit = false;
            posessionNumber = 1;
            playerSmashing = true;
            UpdateRallyScore();
        }
        else if (other.gameObject.name == "Model")
        {
            pongEnemy.RandomSpeedGenerator();

            LobShotRNG();
            playerHit = true;
            enemyHit = false;
            posessionNumber = 1;
            UpdateRallyScore();
        }
        else if (other.gameObject.name == "EnemyBat")
        {
            LobShotRNG();
            enemyHit = true;
            playerHit = false;
            posessionNumber = 2;
            UpdateRallyScore();
        }

        //Als de bal van de enemy is en hij gaat over het netje heen, mag de enemy niet bewegen.
        if(other.gameObject.name == "EnemyMovingTrigger" && posessionNumber == 2)
        {
            pongEnemy.allowedToMove = false;
        }
        //Als de bal niet van de enemy is, en over het netje gaat, mag de enemy bewegen.
        if(other.gameObject.name == "EnemyMovingTrigger" && posessionNumber < 2)
        {
            pongEnemy.allowedToMove = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {            
        //Geef de bal een nummer, gebaseerd op van 'wie' de bal is.
        //Hiermee kan je checken wie de punten moet krijgen.
        if (other.gameObject.name == "PlayerFloor")
        {
            posessionNumber = 1;
        }
        else if (other.gameObject.name == "EnemyFloor")
        {
            posessionNumber = 2;
        }
        
        //Als de unbanned bal de grond raakt, wordt er gekeken van wie hij is. De andere krijg dan een punt.
        if(other.gameObject.name == "Plane" && posessionNumber == 1 && !ballBanned)
        {
            Debug.Log("P2 gets point.");
            ballBanned = true;
            pongscore.UpdateScore(0, 1);
        }
        else if (other.gameObject.name == "Plane" && posessionNumber == 2 && !ballBanned)
        {
            Debug.Log("OUT! P1 gets point.");
            ballBanned = true;
            pongscore.UpdateScore(1, 0);
        }


        //Als de bal het net raakt, krijgt de ander een punt.
        if (other.gameObject.name == "Net" && posessionNumber == 1 && !ballBanned)
        {
            ballBanned = true;
            lobActive = false;
            Debug.Log("NET! EnemyScore + 1");
            pongscore.UpdateScore(0, 1);
        }
        else if (other.gameObject.name == "Net" && posessionNumber == 2 && !ballBanned)
        {
            ballBanned = true;
            lobActive = false;
            Debug.Log("NET! PlayerScore + 1");
            pongscore.UpdateScore(1, 0);
        }        
    }

    void PlayerHitBall()
    {
        //Doe een Smash-check.
        if (playerHit && playerSmashing)
        {
            //Tik de bal vooruit.
            EnemyTarget();
            rb.velocity = Vector3.zero;
            transform.LookAt(smashTarget.transform.position);
            rb.AddForce(transform.forward * (hitPower * smashPowerMultiplier), ForceMode.Impulse);
            
            //Als je een lobhit countert met een smash, krijgt je smash een kleine boost opwaards, om 100% niet te missen.
            if(lobActive)
            {
                rb.AddForce(Vector3.up * smashUpPower, ForceMode.Impulse);       
            }
            playerSmashing = false;
            playerHit = false;
        }
        else if (playerHit)
        {
            EnemyTarget();
            if(lobShot)
            {
                rb.velocity = Vector3.zero;
                transform.LookAt(playerTarget.transform.position);
                rb.AddForce(Vector3.up * hitPower * upMultiplier, ForceMode.Impulse);
                rb.AddForce(transform.forward * lobHitPower, ForceMode.Impulse);
                lobShot = false;
                lobActive = true;
                playerHit = false;
            }
            else if(!lobShot)
            {
                rb.velocity = Vector3.zero;
                transform.LookAt(playerTarget.transform.position);
                rb.AddForce(Vector3.up * hitPower, ForceMode.Impulse);
                rb.AddForce(transform.forward * hitPower, ForceMode.Impulse);
                playerHit = false;
            }
            
        }
        if (enemyHit)
        {
            if(lobShot)
            {
                rb.velocity = Vector3.zero;
                transform.LookAt(enemyTarget.transform.position);
                rb.AddForce(Vector3.up * hitPower * upMultiplier, ForceMode.Impulse);
                rb.AddForce(transform.forward * lobHitPower, ForceMode.Impulse);
                lobActive = true;
                enemyHit = false;
            }
            if(!lobShot)
            {
                rb.velocity = Vector3.zero;
                transform.LookAt(enemyTarget.transform.position);
                rb.AddForce(Vector3.up * hitPower, ForceMode.Impulse);
                rb.AddForce(transform.forward * hitPower, ForceMode.Impulse);
                enemyHit = false;
            }
            
        }
    }

    void UpdateRallyScore()
    {
        rallyScore++;
        GameObject.Find("RallyText").GetComponent<Text>().text = "Rally:\n" + rallyScore;
    }

    void PlayerInput()
    {
        try
        {
            //Houd de touchpad in om te smashen.
            if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetPress(SteamVR_Controller.ButtonMask.Touchpad) || Input.GetKey(KeyCode.V))
            {
                playerCharging = true;
            }
            else if (SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) || Input.GetKeyUp(KeyCode.V))
            {
                playerCharging = false;
            }
        }
        catch
        {
            //Voor debugging kan je ook V inhouden voor een smash.
            if(Input.GetKey(KeyCode.V))
            {
                playerCharging = true;
            }
            if (Input.GetKeyUp(KeyCode.V))
            {
                playerCharging = false;
            }
        }
        
        //Sla de input van het 'touchpad vegen' op in een variabel.
        if (grabPongBat.leftHand.transform.GetChild(0).gameObject.activeInHierarchy == true)
        {            
            tPadInput = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        }
        else if (grabPongBat.rightHand.transform.GetChild(0).gameObject.activeInHierarchy == true)
        {            
            tPadInput = SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        }

        //Verander de PlayerTarget position.
        float z = tPadInput.x * hitrange;
        playerTarget.transform.position = new Vector3(playerTarget.transform.position.x, playerTarget.transform.position.y, z);
        smashTarget.transform.position = new Vector3(smashTarget.transform.position.x, smashTarget.transform.position.y, z);
    }

    void LobShotRNG()
    {
        //Random kans voor een lob (hoge hit)
        if(Random.Range(0, 100) <= lobShotChance)
        {
            lobShot = true;
        }
    }

    void EnemyTarget()
    {
        //Geef de enemyTarget een random positie tussen waar hij gaat schieten.
        float rngPos = Random.Range(-1.3f, 1.3f);
        enemyTarget.transform.localPosition = new Vector3(rngPos, enemyTarget.transform.localPosition.y, enemyTarget.transform.localPosition.z);
    }
}