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
        //"Water plant",
        //"Clean dining table",
        "Clean sofa",
        //"Take a nap",
        //"Wash dishes",

        //"Organize wardrobe",
        //"Pick up clutter",
        //"Laundry",
        //"Finish homeworks",
        //"Eat lunch",

        //"Wash hands",
        //"Brush teeth",
        //"Empty trash bin",
        //"Visit sauna",
        //"Relax on couch",

        //"Clean toilet",
        //"Arrange paintings",
        //"Clean cabinets",
        //"Clean center table (living room)",
        //"Clean bed (room 2)",

        //"Clean shower (restroom 2)",
        //"Arrange chairs (guest hall",
        //"Clean paintings (extra room)",
        //"Clean sink (restroom 2)",
        //"Water plant (extra room)"
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
            case 1: total = 1; break;
            case 2: total = 1; break;
            case 3: total = 1; break;
            case 4: total = 1; break;
            case 5: total = 1; break;
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
        if (Current_Missions.Count == 0 && GameSystem.day < 5 && TempTime.hour < 3 && !gameOver)
        {
            TempTime.stop = true;
            GameSystem.loading = true;
            DoneUI.SetActive(true);
        }

        else if (TempTime.hour >= 3 && !gameOver)
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

        else if (Current_Missions.Count == 0 && GameSystem.day == 5 && TempTime.hour < 3 && !gameOver)
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
