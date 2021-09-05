using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBehavior : MonoBehaviour
{
    private Text timeText;
    private int day = 1;
    private int hour = 0;
    private float minute = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        timeText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(day == 7 && hour == 23 && minute >= 59.0f))
        {
            minute += Time.deltaTime * 25f;

            if (minute >= 60.0f)
            {
                if(!(day == 7 && hour == 23))
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
