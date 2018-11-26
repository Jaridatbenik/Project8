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

            if (target.gameObject.tag == "Snappable" && holdsObject == true)
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
