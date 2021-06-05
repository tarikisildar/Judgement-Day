using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class RoundEndingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Timer timer;

    public void Initialize(int round,float timeToNextStage)
    {
        GetComponent<FadeHandler>().FadeIn(0.5f);
        if (round >= Constants.RoundCount)
        {
            InitGameOver();
        }
        else
        {
            InitNextRound(round);
        }
        timer.Activate(timeToNextStage);
    }

    private void InitGameOver()
    {
        title.text = "Game Over";
        timerText.text = "Room Closing In";
    }

    private void InitNextRound(int round)
    {
        title.text = "Round " + round.ToString() + " Over";
        timerText.text = "Next Round In";
    }
}
