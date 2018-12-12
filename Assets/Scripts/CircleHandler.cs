using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CircleTypes
{
    Teleportation,
    ObjectSelection
}


public class CircleHandler : MonoBehaviour
{
    public Image image;
    float timer = 0;
    float maxTime = 0;
    CircleTypes currentType;
    GameObject gameObjectIn;

    bool inTimer = false;

    //Temp-------------------
   // public GameObject cube;
    //End Temp---------------

    public void SpawnCircle(float timeToShow, CircleTypes type, Action subscribeToThisCompletionEvent, Action subscribeToThisCancelEvent)
    {
        this.maxTime = timeToShow;
        this.currentType = type;
        inTimer = true;

        CircleEventHandler.SubscribeToEvent(subscribeToThisCompletionEvent, subscribeToThisCancelEvent);
    }

    public void SpawnCircle(float timeToShow, CircleTypes type, GameObject gameObj, Action subscribeToThisCompletionEvent, Action subscribeToThisCancelEvent)
    {
        this.maxTime = timeToShow;
        this.currentType = type;
        this.gameObjectIn = gameObj;
        inTimer = true;

        CircleEventHandler.SubscribeToEvent(subscribeToThisCompletionEvent, subscribeToThisCancelEvent);
    }


    void Start()
    {
        image.fillAmount = 0;
    }

    void Update()
    {


        if (inTimer)
        {
            if(timer <= maxTime)
            {
                timer += Time.deltaTime;
                image.fillAmount = timer / maxTime;
                if(currentType == CircleTypes.Teleportation)
                {
                    image.rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2);
                }else
                {
                    //image.rectTransform.position = Camera.main.WorldToScreenPoint(cube.transform.position);
                }
            }else
            {
                ResetCircle();
                FinishCircle();
            }
        }
    }

    public void FinishCircle()
    {
        image.fillAmount = 0;
        CircleEventHandler.RunEvents(true);
    }

    public void CancelCircle()
    {
        ResetCircle();
        CircleEventHandler.RunEvents(false);
    }

    public void ResetCircle()
    {
        inTimer = false;
        timer = 0;
        maxTime = 0;
        image.fillAmount = 0;
    }

    public void TestMethod()
    {
        Debug.Log("hoi");
    }
}

public static class CircleEventHandler
{ 
    public static List<Action> onComplete = new List<Action>();
    public static List<Action> onCancel = new List<Action>();

    public static void SubscribeToEvent(Action complete, Action cancel)
    {
        onComplete.Add(complete);
        onCancel.Add(cancel);
    }

    public static void UnsubscribeFromEvent(Action complete, Action cancel)
    {
        onComplete.Add(complete);
        onCancel.Add(cancel);
    }

    /// <summary>
    /// When you run this method, all methods you have subscribed to will be removed, if you dont want this put false as a paramater
    /// Put in true if you want to run the complete events and false if you want to run the cancel events
    /// </summary>
    public static void RunEvents(bool completed)
    {
        if (completed)
        {
            for (int i = 0; i < onComplete.Count; i++)
            {
                try { onComplete[i].Invoke(); } catch { }
            }

            onComplete.Clear();
        }else
        {
            for (int i = 0; i < onCancel.Count; i++)
            {
                try { onCancel[i].Invoke(); } catch { }
            }

            onCancel.Clear();
        }
    }

    /// <summary>
    /// When you run this method, all methods you have subscribed to will be removed, if you dont want this put false as a paramater
    /// </summary>
    public static void RunEvents(bool completed, bool boolin)
    {
        if (completed)
        {
            for (int i = 0; i < onComplete.Count; i++)
            {
                try { onComplete[i].Invoke(); } catch { }
            }

            if (boolin)
            {
                onComplete.Clear();
            }
        }else
        {
            for (int i = 0; i < onCancel.Count; i++)
            {
                try { onCancel[i].Invoke(); } catch { }
            }

            if (boolin)
            {
                onCancel.Clear();
            }

        }
    }

    /// <summary>
    /// When you run this method nothing happens
    /// </summary>
    public static void NullableMethod() { }
}

