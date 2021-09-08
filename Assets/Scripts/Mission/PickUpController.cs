using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    public Transform player;
    public float pickUpRange;

    public GameObject loadingUI;

    float ticks = 0.0f;
    float loadTime = 0.0f;
    bool loading = false;
    string ongoing_task = "";

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !loading) {
            loading = true;
            ongoing_task = this.tag;
            StartCoroutine(Loading());
        }

        if (loading)
        {
            loadingUI.SetActive(true);
            GameSystem.loading = true;

            SetLoadTime();

            ticks += Time.deltaTime;
            loadingUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1 - (ticks / loadTime);
            if (ticks >= loadTime)
            {
                GameSystem.loading = false;
                loading = false;
                ticks = 0.0f;
            }
        }
    }

    private void PickUp()
    {
        for(int i = 0; i < MissionScript.Current_Missions.Count; i++)
        {
            if(ongoing_task == MissionScript.Current_Missions[i])
            {
                Destroy(MissionScript.textDisplay[i]);
                MissionScript.Current_Missions.RemoveAt(i);
            }
        }
    }

    public IEnumerator Loading()
    {
        SetLoadTime();

        yield return new WaitForSeconds(loadTime);
        loadingUI.SetActive(false);
        PickUp();
    }

    void SetLoadTime()
    {
        switch (ongoing_task)
        {
            case "Clean sofa":
                loadTime = 1.0f;
                break;
            case "Clean table":
                loadTime = 3.0f;
                break;
            case "Water plant":
                loadTime = 1.0f;
                break;
            case "Take a nap":
                loadTime = 1.0f;
                break;
            case "Wash dishes,":
                loadTime = 1.0f;
                break;

            case "Organize wardrobe":
                loadTime = 5.0f;
                break;
            case "Pick up clutter":
                loadTime = 3.0f;
                break;
            case "Laundry":
                loadTime = 1.0f;
                break;
            case "Finish homeworks":
                loadTime = 1.0f;
                break;
            case "Eat lunch":
                loadTime = 1.0f;
                break;

            case "Wash hands":
                loadTime = 5.0f;
                break;
            case "Brush teeth":
                loadTime = 3.0f;
                break;
            case "Empty trash bin":
                loadTime = 1.0f;
                break;
            case "Visit sauna":
                loadTime = 1.0f;
                break;
            case "Relax on couch":
                loadTime = 1.0f;
                break;

            case "Clean toilet":
                loadTime = 5.0f;
                break;
            case "Arrange paintings":
                loadTime = 3.0f;
                break;
            case "Clean cabinets":
                loadTime = 1.0f;
                break;
            case "Clean center table (living room)":
                loadTime = 1.0f;
                break;
            case "Clean bed (room 2)":
                loadTime = 1.0f;
                break;

            case "Clean shower (restroom 2)":
                loadTime = 5.0f;
                break;
            case "Arrange chairs (guest hall":
                loadTime = 3.0f;
                break;
            case "Clean paintings (extra room)":
                loadTime = 1.0f;
                break;
            case "Clean sink (restroom 2)":
                loadTime = 1.0f;
                break;
            case "Water plant (extra room)":
                loadTime = 1.0f;
                break;
        }
    }
}
