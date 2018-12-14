using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    TMP_InputField minutes;
    [SerializeField]
    TMP_InputField seconds;

    [SerializeField]
    SubMenu submenu;

    bool countingDown = false;
    int min_i;
    float sec_i;

    MenuCanvas menuCanvas;
    Animator menu;
    TextMeshProUGUI textmesh;

    SceneSwitching sceneSwitching;

    void Update()
    {
        if (countingDown)
        {
            if (min_i > 0 || sec_i > 1)
            {
                if (sec_i < 0)
                {
                    min_i--;
                    sec_i = 60;
                }
                if (sec_i > 0)
                {
                    sec_i -= Time.deltaTime;
                    //print(min_i + " : " + sec_i);

                    //Update visual timer here.
                    string min = min_i.ToString();
                    if (min.Length == 1)
                        min = "0" + min;

                    string sec = Mathf.FloorToInt(sec_i).ToString();
                    if (sec.Length == 1)
                        sec = "0" + sec;

                    textmesh.text = min + ":" + sec;
                }
            }
            else if (min_i <= 0 && sec_i < 1)
            {
                countingDown = false;
                sceneSwitching.LoadScene();
            }
        }
    }

    void Start()
    {
        menuCanvas = FindObjectOfType<MenuCanvas>();
        menu = menuCanvas.GetComponent<Animator>();
        textmesh = menuCanvas.countDownText.GetComponent<TextMeshProUGUI>();

        sceneSwitching = GetComponent<SceneSwitching>();
    }

    public void StartCountingDown()
    {
        min_i = int.Parse(minutes.text);
        sec_i = float.Parse(seconds.text);

        submenu.Toggle();

        countingDown = true;

        menu.SetTrigger("StartCountDown");
    }
}