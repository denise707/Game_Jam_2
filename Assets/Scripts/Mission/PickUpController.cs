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
            case "Water plant (Center Hall)":
                loadTime = 5.0f;
                break;
            case "Clean dining table":
                loadTime = 5.0f;
                break;
            case "Dust sofa (Living Room)":
                loadTime = 5.0f;
                break;
            case "Take a nap":
                loadTime = 10.0f;
                break;
            case "Wash dishes,":
                loadTime = 5.0f;
                break;

            case "Organize wardrobe":
                loadTime = 10.0f;
                break;
            case "Clean table (Guest Hall)":
                loadTime = 3.0f;
                break;
            case "Arrange blue paintings":
                loadTime = 5.0f;
                break;
            case "Read books":
                loadTime = 10.0f;
                break;
            case "Eat lunch":
                loadTime = 10.0f;
                break;

            case "Wash hands":
                loadTime = 5.0f;
                break;
            case "Brush teeth":
                loadTime = 5.0f;
                break;
            case "Empty trash bin":
                loadTime = 5.0f;
                break;
            case "Visit sauna":
                loadTime = 10.0f;
                break;
            case "Relax on couch (Guest Hall)":
                loadTime = 10.0f;
                break;

            case "Clean toilet":
                loadTime = 5.0f;
                break;
            case "Arrange orange paintings":
                loadTime = 3.0f;
                break;
            case "Fix cabinet (Bedroom)":
                loadTime = 10.0f;
                break;
            case "Clean center table (Living Room)":
                loadTime = 5.0f;
                break;
            case "Clean cabinet (Living Room)":
                loadTime = 5.0f;
                break;

            case "Take shower":
                loadTime = 10.0f;
                break;
            case "Arrange chairs (Guest Hall)":
                loadTime = 5.0f;
                break;
            case "Organize vases (Center Hall)":
                loadTime = 5.0f;
                break;
            case "Clean cabinet (Restroom)":
                loadTime = 5.0f;
                break;
            case "Clean bathtub":
                loadTime = 5.0f;
                break;
        }
    }
}
