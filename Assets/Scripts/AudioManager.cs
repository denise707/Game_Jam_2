using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance;

    public AudioSource audioSource;
    public AudioClip BGM;
    public AudioClip pickupSound;
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

    public void PlayPickupSound()
    {
        AudioSource.PlayClipAtPoint(pickupSound, audioSource.gameObject.transform.position);
    }

    public void StopBGM()
    {
        if(audioSource.clip == BGM)
        {
            audioSource.Stop();
        }
    }    
}
