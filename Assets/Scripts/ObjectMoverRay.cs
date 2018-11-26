using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoverRay : MonoBehaviour
{
    public float timerRay;
    float timer;
    GameObject target;
    public GameObject cameraParent;
    public GameObject targetObject;
    public AIController ac;
    public ObstacleScript ob;
    public bool holdsObject = false;
    ObjectMover om;

    private void Start()
    {
        cameraParent = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        Vector2 crossHeirPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(crossHeirPoint);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit) == true)
        {
            target = raycastHit.collider.gameObject;

            if (target.gameObject.tag == "Obstacle" && (ob.obstacleRight1 || ob.obstacleLeft1) && holdsObject == false)
            {
                timerRay += Time.deltaTime;
                if (timerRay >= 2)
                {
                    om = raycastHit.collider.gameObject.GetComponent<ObjectMover>();
                    timerRay = 0;
                    target.transform.parent = cameraParent.transform;
                    target.layer = 2;
                    holdsObject = true;
                    ob.obstacleLeft1 = false;
                    ob.obstacleRight1 = false;
                }
            }
            else if (target.gameObject.tag == "Obstacle 1" && (ob.obstacleRight2 || ob.obstacleLeft2) && holdsObject == false)
            {
                timerRay += Time.deltaTime;
                if (timerRay >= 2)
                {
                    om = raycastHit.collider.gameObject.GetComponent<ObjectMover>();
                    timerRay = 0;
                    target.transform.parent = cameraParent.transform;
                    target.layer = 2;
                    holdsObject = true;
                    ob.obstacleLeft2 = false;
                    ob.obstacleRight2 = false;
                }
            }

            else if (target.gameObject.tag == "Snappable" && holdsObject == true)
            {
                timer += Time.deltaTime;
                if (timer >= 2)
                {
                    timer = 0;
                    targetObject = target;
                    holdsObject = false;
                    om.GoToObject(targetObject);
                }
            }
            else
            {
                timerRay = 0;
                timer = 0;
            }
        }
    }
}
