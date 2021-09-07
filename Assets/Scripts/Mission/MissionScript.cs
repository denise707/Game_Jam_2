using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionScript : MonoBehaviour
{
    public GameObject textCopy;
    public GameObject textParent;

    public static List<string> Day_1_Missions = new List<string>()
    {
        "Water plant",
        "Clean table",
        "Clean sofa"
    };

    public static List<GameObject> textDisplay = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Day_1_Missions.Count; i++)
        {
            GameObject newMission = GameObject.Instantiate(this.textCopy, this.textParent.transform);
            newMission.GetComponent<Text>().text = Day_1_Missions[i];
            textDisplay.Add(newMission);
        }

        textCopy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
