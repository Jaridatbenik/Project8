using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    //Components
    RectTransform t;
    Animator animator;

    //'speed' Variables.
    [Header("Animation Variables")]
    public float movespeed = 5f;
    public float reanchorspeed = 0.8f;
    public float reformspeed = 5f;
    [Space]

    private bool animationactive = false;
    float movetime = 0;

    //Owner Stuff
    [Header("This submenu can appear from these Objects.")]
    [HideInInspector]
    public RectTransform pressedButton;


    //This is in order of: AnchorMin, AnchorMax, Pivot and LocalScale. (Inspector from top to bottom except for sizeDelta)
    List<Vector2> grownPoints = new List<Vector2>();
    [HideInInspector]
    public List<Vector2> shrunkPoints = new List<Vector2>();
    List<Vector2> targetPoints = new List<Vector2>();

    Vector2 grownPos;
    [HideInInspector]
    public Vector2 shrunkPos;
    Vector2 targetPos;

    public GameObject darkImage;   //Assign in Editor.


    void Start()
    {
        t = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
        StartSmall();

        if (darkImage != null)
            darkImage.SetActive(false);

        gameObject.SetActive(false);
    }

    void StartSmall()
    {
        grownPos = t.position;

        FillPointsList(grownPoints, t);
        FillPointsList(shrunkPoints, pressedButton);

        t.anchorMin = shrunkPoints[0];
        t.anchorMax = shrunkPoints[1];
        t.pivot = shrunkPoints[2];
        t.sizeDelta = shrunkPoints[3];
        t.localScale = shrunkPoints[4];

        t.position = shrunkPos;
    }

    /*void ChangeParentPanelPoints()
    {
        RectTransform panel = transform.parent.GetComponent<RectTransform>();
        RectTransform ownerPanel = pressedButton.parent.GetComponent<RectTransform>();

        panel.anchorMin = ownerPanel.anchorMin;
        panel.anchorMax = ownerPanel.anchorMax;
        panel.pivot = ownerPanel.pivot;
        panel.sizeDelta = ownerPanel.sizeDelta;
        panel.localScale = ownerPanel.localScale;

        panel.position = ownerPanel.position;
    }*/

    public void FillPointsList(List<Vector2> list, RectTransform rect)
    {
        list.Add(rect.anchorMin);
        list.Add(rect.anchorMax);
        list.Add(rect.pivot);

        if (rect == t)
        {
            list.Add(rect.sizeDelta);
            list.Add(rect.localScale);
        }
        else
        {
            list.Add(Vector2.zero);
            list.Add(Vector2.zero);
        }
    }

    public void Toggle()
    {
        t = GetComponent<RectTransform>();
        ToggleImage();   
        Vector2 currentPos = t.position;

        if (!animationactive)
        {
            movetime = 0;
            SetAnimator(false);
            if (currentPos == grownPos)
            {
                targetPoints = shrunkPoints;
                targetPos = shrunkPos;
                animationactive = true;
            }
            else
            {
                t.position = shrunkPos;
                
                targetPoints = grownPoints;
                targetPos = grownPos;
                animationactive = true;
            }
        }
    }

    void Update()
    {
        Vector2 currentPos = t.position;

        if (animationactive && currentPos != targetPos)
        {
            movetime += Time.unscaledDeltaTime;            

            t.anchorMin = Vector2.Lerp(t.anchorMin, targetPoints[0], reanchorspeed * movetime);
            t.anchorMax = Vector2.Lerp(t.anchorMax, targetPoints[1], reanchorspeed * movetime);
            t.pivot = Vector2.Lerp(t.pivot, targetPoints[2], reanchorspeed * movetime);
            
            t.sizeDelta = Vector3.Lerp(t.sizeDelta, targetPoints[3], reformspeed * movetime);
            t.localScale = Vector2.Lerp(t.localScale, targetPoints[4], reformspeed * movetime);
            t.position = Vector2.Lerp(t.position, targetPos, movespeed * movetime);
        }
        else if (animationactive && currentPos == targetPos)
        {            
            animationactive = false;
        }
    }
    public void SetAnimator(bool b)
    {
        if (animator != null)
            animator.enabled = b;
    }



    public void ToggleImage()
    {
        if (darkImage != null)
        {
            darkImage.gameObject.SetActive(!darkImage.activeInHierarchy);
        }
    }
}