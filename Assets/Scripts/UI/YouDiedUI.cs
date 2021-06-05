using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class YouDiedUI : MonoBehaviour
{
    [SerializeField] private Timer timer;

    public void Initialize(float time)
    {
        GetComponent<FadeHandler>().FadeIn(0.5f);
        timer.Activate(time);
    }
}
