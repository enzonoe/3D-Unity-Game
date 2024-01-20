using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenRadar : MonoBehaviour
{
    public bool isImgOn;
    public GameObject radar;
    public GameObject radarLight;

    void Start()
    {
        if (radar != null && radarLight != null)
        {
            radar.SetActive(false);
            radarLight.SetActive(false);
            isImgOn = false;
        }
        else
        {
            Debug.LogError("Radar GameObject is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //Debug.Log("The Button i pressed");
            if (isImgOn)
            {
                radar.SetActive(false);
                radarLight.SetActive(false);
                isImgOn = false;
            }
            else
            {
                radar.SetActive(true);
                radarLight.SetActive(true);
                isImgOn = true;
            }
        }
    }
}
