using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class ShakeWorldUI : MonoBehaviour
{
    Transform t;

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
        t = transform;                
    }

    public void GoShake()
    {
        startPos = t.localPosition;

        shake = true;
        shakeTime = 0;
    }

    void Update()
    {
        if (shake && shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            Vector3 rndm = Random.insideUnitSphere * shakeIntensity;
            t.localPosition = new Vector3(rndm.x, rndm.y, t.localPosition.z);
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