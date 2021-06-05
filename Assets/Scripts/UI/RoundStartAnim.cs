using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RoundStartAnim : Singleton<RoundStartAnim>
    {
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private Transform timer;
        private float delay = 1f;
        private float moveTime = 1f;
        private int up = 400;

        private Vector3 timerPos;
        private Vector3 roundPos;

        private void Awake()
        {
            timerPos = timer.position;
            roundPos = roundText.transform.position;
            roundText.transform.position = roundPos + Vector3.up * up;
            timer.position = timerPos + Vector3.up * up;
        }

        private void OnEnable()
        {
            roundText.transform.position = roundPos + Vector3.up * up;
            timer.position = timerPos + Vector3.up * up;
        }

        public void Activate(int round, float time)
        {
            roundText.text = "Round " + round.ToString();
            timer.GetChild(0).GetComponent<Timer>().Activate(time);

            var seq = DOTween.Sequence();


            seq.AppendInterval(0.5f);
            seq.Append(roundText.transform.DOMove(roundPos, moveTime)).AppendInterval(delay);
            seq.Append(roundText.transform.DOMove(roundPos + Vector3.up * up, moveTime)).AppendInterval(delay/2f);
            seq.Append(timer.DOMove(timerPos, moveTime));

            seq.Play();

        }
    }
}