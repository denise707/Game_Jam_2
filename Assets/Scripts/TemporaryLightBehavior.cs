using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryLightBehavior : MonoBehaviour
{
    private float lightTimer = 0.0f;
    private float lightDuration = 45.0f;
    public bool isLighted = false;

    // Start is called before the first frame update
    void Start()
    {
        isLighted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLighted)
        {
            PerformLighting();
        }
    }

    void LightFire()
    {
        isLighted = true;
    }

    void PerformLighting()
    {
        lightTimer += Time.deltaTime;

        if (lightTimer >= lightDuration)
        {
            isLighted = false;
            lightTimer = 0.0f;
        }
    }    
}
