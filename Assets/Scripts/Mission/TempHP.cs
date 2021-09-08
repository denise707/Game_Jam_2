using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TempHP : MonoBehaviour
{
    public List<GameObject> LivesRemaining;
    public List<GameObject> LivesExtinguished;

    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //if(timer > 15.0f)
        //{
        //    if(LivesRemaining.Count > 0)
        //        TakeDamage();

        //    timer = 0.0f;
        //}
    }

    public void TakeDamage()
    {
        GameObject life = LivesRemaining[LivesRemaining.Count - 1];

        Color dead = life.GetComponent<Image>().color;
        dead.a = 0.4f;
        life.GetComponent<Image>().color = dead;

        LivesExtinguished.Add(life);
        LivesRemaining.Remove(life);

        if (LivesRemaining.Count == 0)
            EventBroadcaster.Instance.PostEvent(EventNames.ON_PLAYER_LOSE);

    }
}
