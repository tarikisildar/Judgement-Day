using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private bool active = false;
    private float time = 0f;
    //PhotonNetwork.ServerTimestamp
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Activate(float t)
    {
        time = t;
        active = true;
    }

    private void Update()
    {
        text.text = time.ToString("n1");
        if (active && time > 0)
        {
            time = Mathf.Max(time - Time.deltaTime, 0f);
        }

        if (time < 4)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
        
    }
}
