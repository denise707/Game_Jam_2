using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionScript : MonoBehaviour
{
    public GameObject textCopy;
    public GameObject textParent;

    //UI
    public GameObject DoneUI;
    public GameObject GameOverUI;
    public Text message;
    public GameObject OvertimeUI;

    public TempHP playerHealth;
    bool gameOver = false;

    List<string> Possible_Missions = new List<string>()
    {
        "Water plant (Center Hall)",
        "Clean dining table",
        "Dust sofa (Living Room)",
        "Take a nap",
        "Wash dishes",

        "Organize wardrobe",
        "Clean table (Guest Hall)",
        "Arrange blue paintings",
        "Read books",
        "Eat lunch",

        "Wash hands",
        "Brush teeth",
        "Empty trash bin",
        "Visit sauna",
        "Relax on couch (Guest Hall)",

        "Clean toilet",
        "Arrange orange paintings",
        "Fix cabinet (Bedroom)",
        "Clean center table (Living Room)",
        "Clean cabinet (Living Room)",

        "Take shower",
        "Arrange chairs (Guest Hall)",
        "Organize vases (Center Hall)",
        "Clean cabinet (Restroom)",
        "Clean bathtub"
    };

    public static List<string> Current_Missions = new List<string>();

    public static List<GameObject> textDisplay = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        textCopy.SetActive(false);

        GenerateList();

        for (int i = 0; i < Current_Missions.Count; i++)
        {
            GameObject newMission = GameObject.Instantiate(this.textCopy, this.textParent.transform);
            newMission.GetComponent<Text>().text = Current_Missions[i];
            textDisplay.Add(newMission);
            newMission.SetActive(true);
        }

        EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_WIN, this.onGameWin);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_PLAYER_LOSE, this.onGameOver);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_PLAYER_WIN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_PLAYER_LOSE);
    }

    void GenerateList()
    {
        
        int total = 0;
        List<string> Temp_Missions = new List<string>(Possible_Missions);
        Current_Missions.Clear();
        textDisplay.Clear();

        switch (GameSystem.day)
        {
            case 1: total = 5; break;
            case 2: total = 10; break;
            case 3: total = 15; break;
            case 4: total = 20; break;
            case 5: total = 25; break;
        }

        for (int i = 0; i < total; i++)
        {
            int index = Random.Range(0, Temp_Missions.Count);
            Current_Missions.Add(Temp_Missions[index]);
            Temp_Missions.RemoveAt(index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool dayOver = (TempTime.hour == 23 && TempTime.minute >= 59);
        if (Current_Missions.Count == 0 && GameSystem.day < 5 && !dayOver && !gameOver)
        {
            TempTime.stop = true;
            GameSystem.loading = true;
            DoneUI.SetActive(true);
        }

        else if (dayOver && !gameOver)
        {
            TempTime.stop = true;
            GameSystem.loading = true;

            if(playerHealth.LivesRemaining.Count > 1)
            {
                OvertimeUI.SetActive(true);
            }

            else
            {
                playerHealth.TakeDamage();
                gameOver = true;
            }            
        }

        else if (Current_Missions.Count == 0 && GameSystem.day == 5 && !dayOver && !gameOver)
        {
            TempTime.stop = true;
            GameSystem.loading = true;
            EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_WIN);
        }
    }

    public void OnNextDay(GameObject Done)
    {
        //Debug.Log("Good");

        TempTime.stop = false;
        TempTime.hour = 24;
        TempTime.minute = 60;

        Done.SetActive(false);

        GameSystem.day++;
        GameSystem.loading = false;

        //Debug.Log(GameSystem.day);

        GenerateList();

        for (int i = 0; i < Current_Missions.Count; i++)
        {
            GameObject newMission = GameObject.Instantiate(this.textCopy, this.textParent.transform);
            newMission.GetComponent<Text>().text = Current_Missions[i];
            textDisplay.Add(newMission);
            newMission.SetActive(true);
        }       
    }

    public void onMinimize(GameObject TaskUI)
    {
        if (TaskUI.activeSelf)
        {
            TaskUI.SetActive(false);
        }
        else
        {
            TaskUI.SetActive(true);
        }
    }

    public void onGameOver()
    {
        GameOverUI.SetActive(true);
        message.text = "Game Over";
    }

    public void onGameWin()
    {
        GameOverUI.SetActive(true);
        message.text = "Game Win";
    }

    public void onClose(GameObject window)
    {
        window.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void onOvertime(GameObject overtime)
    {
        overtime.SetActive(false);
        playerHealth.TakeDamage();

        TempTime.stop = false;
        TempTime.day = GameSystem.day - 1;
        TempTime.hour = 24;
        TempTime.minute = 60;

        GameSystem.loading = false;



        for (int i = 0; i < MissionScript.textDisplay.Count; i++)
        {
            Destroy(MissionScript.textDisplay[i]);

        }

        while(MissionScript.Current_Missions.Count > 0)
        {
            
            MissionScript.Current_Missions.RemoveAt(0);
        }

        GenerateList();

        for (int i = 0; i < Current_Missions.Count; i++)
        {
            GameObject newMission = GameObject.Instantiate(this.textCopy, this.textParent.transform);
            newMission.GetComponent<Text>().text = Current_Missions[i];
            textDisplay.Add(newMission);
            newMission.SetActive(true);
        }
    }
}
