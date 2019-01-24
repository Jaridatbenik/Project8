using System.Collections.Generic;
using UnityEngine;

//This Script should be placed on the prefab that gets spawned by SubMenuButton-class
public class SubMenu : MonoBehaviour
{
    //Components
    RectTransform t;    

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
    public RectTransform pressedButton;     //This is the button that the submenu will 'hide' in.


    //This is in order of: AnchorMin, AnchorMax, Pivot and LocalScale. (Inspector from top to bottom except for sizeDelta)
    List<Vector2> grownPoints = new List<Vector2>();
    [HideInInspector]
    public List<Vector2> shrunkPoints = new List<Vector2>();
    List<Vector2> targetPoints = new List<Vector2>();

    Vector2 grownPos;
    [HideInInspector]
    public Vector2 shrunkPos;
    Vector2 targetPos;

    void Start()
    {
        t = GetComponent<RectTransform>();                          
        //Menu gets spawned in at start and should start small (and switch when toggled).
        StartSmall();                    

        gameObject.SetActive(false);
    }

    //
    void StartSmall()
    {
        //We start big but want to start small.
        grownPos = t.position;

        //We'll save the 'big' data (we're big atm) so we can switch later on.
        FillPointsList(grownPoints, t);
        //The 2nd parameter is the position. We wanna hide in the last pressed button
        FillPointsList(shrunkPoints, pressedButton);    

        //Shrunkpoints are already setup. We're gonna change everything to be small.
        t.anchorMin = shrunkPoints[0];
        t.anchorMax = shrunkPoints[1];
        t.pivot = shrunkPoints[2];
        t.sizeDelta = shrunkPoints[3];
        t.localScale = shrunkPoints[4];

        //Position needs to be last as it's based off of anchors and pivots.
        t.position = shrunkPos;
    }

    //We'll fill the given lists here.
    public void FillPointsList(List<Vector2> list, RectTransform rect)
    {        
        list.Add(rect.anchorMin);
        list.Add(rect.anchorMax);
        list.Add(rect.pivot);

        //if rect ==t, that means we're saving our big-data.
        //We want to use size and scale (scale will work on children while sizeDelta won't, so we use both).
        if (rect == t)
        {
            list.Add(rect.sizeDelta);
            list.Add(rect.localScale);
        }
        else
        {
            //If we're saving the small-data, we can just use 0 for the sizeDelta and scale so it won't be visible.
            list.Add(Vector2.zero);
            list.Add(Vector2.zero);
        }
    }

    //Toggle will switch to 'the other state'.
    public void Toggle()
    {
        //We need to make sure we have THIS object's RectTransform-reference so we'll GetComponent again.
        t = GetComponent<RectTransform>();        
        //Saving our current position because it might change.
        Vector2 currentPos = t.position;

        if (!animationactive)
        {
            movetime = 0;            
            //If we're big, we'll set the variables to start getting small and vice versa.
            if (currentPos == grownPos)
            {
                //Our target will be set to reflect the small data. The Update will stretch the object.
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
        //We'll save the position before changing it.
        Vector2 currentPos = t.position;

        if (animationactive && currentPos != targetPos) //If we're 'allowed' to animate and it's not done yet...
        {
            //We'll up the moveTime in unscaled Time, because this will need to work in potential pause-screens as well.
            movetime += Time.unscaledDeltaTime;            

            //Change the transform data to move towards the target.
            t.anchorMin = Vector2.Lerp(t.anchorMin, targetPoints[0], reanchorspeed * movetime);
            t.anchorMax = Vector2.Lerp(t.anchorMax, targetPoints[1], reanchorspeed * movetime);
            t.pivot = Vector2.Lerp(t.pivot, targetPoints[2], reanchorspeed * movetime);
            
            t.sizeDelta = Vector3.Lerp(t.sizeDelta, targetPoints[3], reformspeed * movetime);
            t.localScale = Vector2.Lerp(t.localScale, targetPoints[4], reformspeed * movetime);

            //Position NEEDS to be last as it's based on the other factors and because it's the condition.
            t.position = Vector2.Lerp(t.position, targetPos, movespeed * movetime);
        }
        else if (animationactive && currentPos == targetPos)
        {            
            //When the object has been reformed, disable the animation.
            animationactive = false;
        }
    }
}