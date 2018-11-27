using TMPro;
using UnityEngine;

public class ShowTimer : MonoBehaviour {


    [SerializeField]TextMeshProUGUI textfield;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void FixedUpdate()
    {
        string sec = Mathf.RoundToInt(timer.secondsTimer).ToString();
        string min = timer.minutesTimer.ToString();        

        min = AddExtraZero(min);
        sec = AddExtraZero(sec);

        textfield.text = min + ":" + sec;
    }

    string AddExtraZero(string s)
    {
        if(s.Length == 1)
        {
            s = "0" + s;
        }
        return s;
    }
}
