using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Dit script moet op de objecten die zullen despawnen wanneer ze met een object
//colliden dat "DespawnBarrier als tag heeft.
public class BallRespawn : MonoBehaviour
{
    public bool vanishing = false;
    public float vanishingSpeed = 0.4f;
    private bool onlyPlayOnce = true;

    private Vector3 spawnPosition;
    private GameObject prefab;      

    void Update()
    {
        //Vanish wordt in de update aangeroepen zodat, wanneer de 'vanishing' bool aangaat, ---
        //de Lerp kan werken.
        Vanish();

        //Als je op de linker of rechter controller's touchpad drukt, gaat vanishing ook aan.
        if((SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost)).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) ||
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) || Input.GetKeyDown(KeyCode.R)) && SceneManager.GetActiveScene().name != "PingPong")
        {
            vanishing = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        //Als de barrier geraakt wordt, moet de bool op true. Op dat begint het 'vanishen'.
        if (other.gameObject.tag == "DespawnBarrier")
        {
            vanishing = true;
        }
    }

    void Vanish()
    {
        //Zodra de bal de Barrier raakt, gaat deze bool op true en wordt de code gebruikt.
        if (vanishing)
        {
            //Sla de MeshRenderer op zodat deze niet 4x getypt hoeft te worden.
            MeshRenderer mr = GetComponent<MeshRenderer>();
            float a = mr.material.color.a;
            float r = mr.material.color.r;
            float g = mr.material.color.g;
            float b = mr.material.color.b;

            //Sla de huidige alpha-waarde op in een variabel die niet verandert, ---
            //Zodat de snelheid van de Lerp niet constant wordt aangepast.
            float startValue = a;
            a = Mathf.Lerp(startValue, 0, vanishingSpeed * Time.deltaTime);

            //Zet de material z'n kleur naar dezelfde kleuren (en nieuwe alphawaarde)
            mr.material.color = new Color(r, g, b, a);

            //Als de alphawaarde onder 0.15 is gezakt, wordt de functie (één keer) aangeroepen.
            if (a <= 0.15 && onlyPlayOnce)
            {
                ReplaceObject();
                onlyPlayOnce = false;
            }
        }
    }

    //Nieuwe bal wordt gemaakt en de huidige (waar het script op zit) gewist.
    public void ReplaceObject()
    {
        GameObject replacement = prefab.gameObject;
        Instantiate(replacement, spawnPosition, Quaternion.identity);

        Destroy(gameObject);        
    }

    void Start()
    {
        spawnPosition = gameObject.transform.position;

        if(SceneManager.GetActiveScene().name == "3-pointer" || SceneManager.GetActiveScene().name == "3-pointer 1")
        {
            prefab = (GameObject)Resources.Load("Basketball");
        }
        if(SceneManager.GetActiveScene().name == "Bowlen")
        {
            prefab = (GameObject)Resources.Load("BowlingBall");
        }      
        if(SceneManager.GetActiveScene().name == "PingPong")
        {
            prefab = (GameObject)Resources.Load("PingPongBall");
            GameObject.Find("EnemyBat").GetComponent<PongEnemy>().ball = gameObject;
        }  
    }
}