using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICallbacks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Room 1");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnOptions(GameObject options)
    {
        Debug.Log("Hello");
        Time.timeScale = 0;
        options.SetActive(true);
    }

    public void OnResume(GameObject options)
    {
        Time.timeScale = 1;
        options.SetActive(false);
    }

    public void OnMainMenu(GameObject options)
    {
        Time.timeScale = 0;
        options.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
