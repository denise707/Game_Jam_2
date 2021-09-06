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
            Debug.Log(this.tag);
        }

        if (loading)
        {
            loadingUI.SetActive(true);
            GameSystem.loading = true;

            switch (ongoing_task)
            {
                case "Clean sofa":
                    loadTime = 8.0f;
                    break;
                case "Clean table":
                    loadTime = 3.0f;
                    break;
                case "Water plant":
                    loadTime = 1.0f;
                    break;
            }

            Debug.Log(ongoing_task);

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
        for(int i = 0; i < MissionScript.Day_1_Missions.Count; i++)
        {
            if(ongoing_task == MissionScript.Day_1_Missions[i])
            {
                Destroy(MissionScript.textDisplay[i]);
            }
        }
    }

    public IEnumerator Loading()
    {
        switch (ongoing_task)
        {
            case "Clean sofa":
                loadTime = 8.0f;
                break;
            case "Clean table":
                loadTime = 3.0f;
                break;
            case "Water plant":
                loadTime = 1.0f;
                break;
        }

        yield return new WaitForSeconds(loadTime);
        loadingUI.SetActive(false);
        PickUp();
    }
}
