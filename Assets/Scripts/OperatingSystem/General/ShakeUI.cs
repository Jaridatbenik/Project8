using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class ShakeUI : MonoBehaviour
{
    RectTransform t;

    Vector3 startPos;

    float shakeTime = 0f;
    bool shake = false;

    [SerializeField]
    float shakeDuration = 0.8f;
    [SerializeField]
    float shakeIntensity = 1;
    [SerializeField]
    float decreaseFactor = 1;

    public UnityEvent callAfterShake;

    void Start()
    {
        t = GetComponent<RectTransform>();                
    }

    public void GoShake()
    {
        if (t == null)
            t = GetComponent<RectTransform>();

        startPos = t.localPosition;

        shake = true;
        shakeTime = 0;
    }

    void Update()
    {
        if (shake && shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            t.localPosition = Random.insideUnitSphere * shakeIntensity;
        }
        else if(shake)
        {
            shake = false;
            shakeTime = 0;

            t.localPosition = startPos;
            callAfterShake.Invoke();
        }
    }
}