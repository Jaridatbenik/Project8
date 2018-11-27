using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [HideInInspector]
    public float secondsTimer = 0;
    [HideInInspector]
    public int minutesTimer = 0;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        float speedMultiplier = 1;
        if (Input.GetButton("Jump"))
        {
            speedMultiplier = 20;
        }

        secondsTimer += Time.unscaledDeltaTime * speedMultiplier;

        if (Mathf.RoundToInt(secondsTimer) == 60)
        {
            secondsTimer = 0;
            minutesTimer++;
        }        
    }
}