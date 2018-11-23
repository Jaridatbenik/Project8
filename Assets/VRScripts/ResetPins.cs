using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Dit script zit het "Pins" object. Hier zitten alle BowlingPins in.
public class ResetPins : MonoBehaviour
{
    BallRespawn ballRespawn;

    public GameObject newPins;
    private Vector3 pinsSpawn;

    private bool vanish = false;
    public float vanishingSpeed = 0.35f;
    private float yStartRotation;

    bool onlyPlayOnce = true;
    float score;

    public float finalscore;

    public List<GameObject> children = new List<GameObject>();

    void Start()
    {
        //Sla de spawn-info alvast op.
        pinsSpawn = gameObject.transform.position;
        newPins = (GameObject)Resources.Load("Pins");
        yStartRotation = gameObject.transform.rotation.eulerAngles.y;

        //Voeg alle (actieve) children toe in een lijst.
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeInHierarchy)
            {
                children.Add(transform.GetChild(i).gameObject);
            }            
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Unpicke")
        {
            //Als een 'throwable' item dit raakt, zal die despawnen.
            vanish = true;                     
            ballRespawn = other.gameObject.GetComponent<BallRespawn>();
            ballRespawn.vanishing = true;
            ballRespawn.vanishingSpeed = vanishingSpeed;
        }
    }

    void Update()
    {
        //Als Vanish aan staat...
        if(vanish)
        {
            //Pak alle child-objects van alle kinderen (grandchildren)
            for (int i = 0; i < children.Count; i++)
            {   
                for(int c = 0; c < children[i].transform.childCount; c++)
                {
                    //Pak de meshrenderer van ieder grandchild-object.
                    MeshRenderer mr = transform.GetChild(i).transform.GetChild(c).GetComponent<MeshRenderer>();
                    float a = mr.material.color.a;
                    float r = mr.material.color.r;
                    float g = mr.material.color.g;
                    float b = mr.material.color.b;

                    //verlaag geleidelijk de waarde van "a".
                    float startValue = a;
                    a = Mathf.Lerp(startValue, 0, vanishingSpeed * Time.deltaTime);

                    //Update de kleur.
                    mr.material.color = new Color(r, g, b, a);

                    //Als de kleur doorzichtig genoeg is, kan de score worden aangepast.
                    if (a <= 0.15 && onlyPlayOnce)
                    {
                        AdjustScore();
                        onlyPlayOnce = false;
                    }
                }
            }
        }
    }

    void AdjustScore()
    {
        //Check per pion de rotatie.
        for (int i = 0; i < children.Count; i++)
        {        
            //Check hoeveel pionnen niet meer stilstaan.    
            if(children[i].transform.rotation.x == 0 && children[i].transform.rotation.z == 0)
            {
                Debug.Log(children[i].gameObject.name + " is still up.");
            }
            else if(children[i].transform.rotation.x != 0 && children[i].transform.rotation.z != 0)
            {
                Debug.Log(children[i].gameObject.name + " fell down.");
                score++;
            }
            //Update de tekst.
            GameObject.Find("Score").GetComponent<Score>().score = (int)score;
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Score:\n" + score;
            
        }
        ReplacePins();
    }

    void ReplacePins()
    {
        //Spawn nieuwe pins, wis de oude.
        GameObject replacement = newPins.gameObject;
        Instantiate(replacement, pinsSpawn, Quaternion.Euler(0, yStartRotation, 0));
        Destroy(gameObject);
    }
}