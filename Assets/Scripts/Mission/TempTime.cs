using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempTime : MonoBehaviour
{
    public Text timeText;
    public static int day = 1;
    public static int hour = 0;
    public static float minute = 0.0f;
    public static bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        day = 1;
        hour = 0;
        minute = 0.0f;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(day == 7 && hour == 23 && minute >= 59.0f) && !stop)
        {
            minute += Time.deltaTime * 25f;

            if (minute >= 60.0f)
            {
                if (!(day == 7 && hour == 23))
                {
                    hour++;
                    minute = 0.0f;
                }

                else
                {
                    minute = 59.0f;
                }
            }

            if (hour >= 24)
            {
                day++;
                hour = 0;
            }
        }

        timeText.text = "Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00");
    }
}
