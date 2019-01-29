using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MegaHandler : MonoBehaviour
{

    Sequensing sec;

    public GameObject gameButtons;
    public GameObject bodyPartsObject;
    public GameObject ultimateRotationObject;

    bool rotatedVoorkant = true;
    [HideInInspector]
    public bool canRotate = false;

    [HideInInspector]
    public List<GameObject> inGameButtons = new List<GameObject>();

    public List<Material> buttonMaterials = new List<Material>();

    public List<GameObject> bodyParts = new List<GameObject>();

    int currentlyBlue = -1;

    public float setTimerToThis = 2.8f;

    float timer = 0;

    int fouten = 2;
    bool moestOpnieuwBeginnen = false;

    public Text foutenText;

    void Start()
    {
        sec = GetComponent<Sequensing>();
        inGameButtons.Clear();
        bodyParts.Clear();
        for (int i = 0; i < gameButtons.transform.childCount; i++)
        {
            inGameButtons.Add(gameButtons.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < bodyPartsObject.transform.childCount; i++)
        {
            bodyParts.Add(bodyPartsObject.transform.GetChild(i).gameObject);
        }
        foutenText.text = moestOpnieuwBeginnen ? (fouten > 1 ? "Je moest opnieuw beginnen, je hebt nog " + fouten + " fouten over" : "Je moest opnieuw beginnen, je hebt nog " + fouten + " fout over") : (fouten > 1 ? "Je hebt nog " + fouten + " fouten over" : "Je hebt nog " + fouten + " fout over");
    }

    public int GetChildNum(GameObject obj)
    {
        for (int i = 0; i < inGameButtons.Count; i++)
        {
            if (inGameButtons[i] == obj)
            {
                return i;
            }
        }
        return -1;
    }

    public void MakeButtonBlue(int butt)
    {
        currentlyBlue = butt;
        if (butt == -1)
        {
            for (int i = 0; i < inGameButtons.Count; i++)
            {
                inGameButtons[i].transform.GetChild(1).GetComponent<MeshRenderer>().material = buttonMaterials[0];
            }
        }
        else
        {
            for (int i = 0; i < inGameButtons.Count; i++)
            {
                inGameButtons[i].transform.GetChild(1).GetComponent<MeshRenderer>().material = buttonMaterials[butt == i ? 1 : 0];
            }
        }
    }

    public void CheckIfCorrect()
    {
        if (sec.currentBodyPart == currentlyBlue)
        {
            if (sec.currentBodyPart != 7)
            {
                bodyParts[currentlyBlue].transform.SetParent(bodyParts[currentlyBlue].GetComponent<BodyPartTeleportPosition>().attachTo.transform);
                bodyParts[currentlyBlue].transform.localPosition = Vector3.zero;
                bodyParts[currentlyBlue].transform.localRotation = new Quaternion(0, 0, 0, 0);
                inGameButtons[currentlyBlue].transform.GetChild(0).gameObject.SetActive(false);
                inGameButtons[currentlyBlue].transform.GetChild(1).gameObject.SetActive(false);
                sec.UpCount();
            }
            Debug.Log("yeah");
        }
        else
        {
            fouten--;
            

            if(fouten <= 0)
            {
                moestOpnieuwBeginnen = true;
                fouten = 2;
                ResetRobot();
            }

            foutenText.text = moestOpnieuwBeginnen ? (fouten > 1 ? "Je moest opnieuw beginnen, je hebt nog " + fouten + " fouten over" : "Je moest opnieuw beginnen, je hebt nog " + fouten + " fout over") : (fouten > 1 ? "Je hebt nog " + fouten + " fouten over" : "Je hebt nog " + fouten + " fout over");
            Debug.Log("no");
        }
    }

    public void DraaiRobot()
    {
        timer = setTimerToThis;
        rotatedVoorkant = !rotatedVoorkant;
        canRotate = true;
    }

    public void ResetRobot()
    {
        ResetButtons();
        ResetRobotParts();
        sec.currentBodyPart = 0;
        ultimateRotationObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        for (int i = 0; i < FindObjectsOfType<KabelData>().Length; i++)
        {
            Destroy(FindObjectsOfType<KabelData>()[i].gameObject);
        }

        for (int i = 0; i < FindObjectsOfType<SpoelHandler>().Length; i++)
        {
            if (FindObjectsOfType<SpoelHandler>()[i].hasCableAttached)
            {
                FindObjectsOfType<SpoelHandler>()[i].hasCableAttached = false;
            }
        }
        Start();
    }

    void ResetButtons()
    {
        for (int i = 0; i < inGameButtons.Count; i++)
        {
            inGameButtons[i].transform.GetChild(0).gameObject.SetActive(true);
            inGameButtons[i].transform.GetChild(1).gameObject.SetActive(true);
        }
        MakeButtonBlue(-1);
    }

    void ResetRobotParts()
    {
        bodyPartsObject.transform.DetachChildren();
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].transform.SetParent(bodyPartsObject.transform);
            try
            {
                bodyParts[i].transform.position = bodyParts[i].GetComponent<BodyPartTeleportPosition>().startPos;
                bodyParts[i].transform.rotation = bodyParts[i].GetComponent<BodyPartTeleportPosition>().startRot;
            }
            catch { }
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            DraaiRobot();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ultimateRotationObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ResetRobot();
        }
#endif
        if (canRotate)
        {
            float rot;
            if (rotatedVoorkant)
            {
                rot = -(180 / setTimerToThis) * Time.deltaTime;
            }
            else
            {
                rot = ((180 / setTimerToThis) * Time.deltaTime);
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                ultimateRotationObject.transform.Rotate(new Vector3(0, rot, 0), Space.Self);
            }
            else
            {
                canRotate = false;
            }

        }
    }
}
