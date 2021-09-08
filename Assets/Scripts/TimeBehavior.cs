using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBehavior : MonoBehaviour
{
    [SerializeField] GameObject sun = null;

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
            minute += Time.deltaTime * 2f;

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

        if(sun != null)
            UpdateSunPos();
    }

    private void UpdateSunPos()
    {
        Vector3 sunPos = Vector3.zero;
        sunPos.x = 360 * (((60 * hour) + minute)/(60 * 24));
        Debug.Log(sunPos.x);
        sun.transform.eulerAngles = sunPos;
    }
}
