using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance;

    public AudioSource audioSource;
    
    
    public AudioClip BGM;
    public AudioClip StartTask;
    public AudioClip EndTask;
    public AudioClip LoseLife;
    public AudioClip LoseGame;
    public AudioClip WinGame;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip sound)
    {
        if(sound != null)
            AudioSource.PlayClipAtPoint(sound, audioSource.gameObject.transform.position, audioSource.volume);
    }

    public void PlayBGM()
    {
        audioSource.clip = BGM;
        audioSource.Play();
    }

    public void StopBGM()
    {
        if(audioSource.clip == BGM)
        {
            audioSource.Stop();
        }
    }    
}
