using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    string minutes;
    string seconds;
    string miliseconds;

    // Update is called once per frame
    void Update()
    {
        formatTimer();
        GetComponent<Text>().text = "0" + minutes + ":" + seconds + ":" + miliseconds;
    }

    void formatTimer()
    {
        float time = GameEvents.gameTimer;
        int min;
        int sec;
        int mil;

        if (time >= 60)
        {
            min = (int)time / 60;     
        }
        else
        {
            min = 0;
        }

        minutes = "" + min;

        sec = (int)time - (min * 60);
        mil = (int)((time - (min * 60) - sec) * 100);

        if(sec < 10)
        {
            seconds = "0" + sec;
        }
        else
        {
            seconds = "" + sec;
        }

        if (mil < 10)
        {
            miliseconds = "0" + mil;
        }
        else
        {
            miliseconds = "" + mil;
        }
    }
}
