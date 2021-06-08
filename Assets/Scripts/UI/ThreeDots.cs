using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ThreeDots : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private float time = 0.3f;
        private float curTime = 0;
        private int dotCount = 0;
        private string originalText;
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            originalText = text.text;
        }

        void Update()
        {
            curTime += Time.deltaTime;
            if (curTime > time)
            {
                text.text = originalText + new string('.',dotCount+1);
                dotCount = (dotCount + 1) % 3;
                curTime = 0;
            }
        }

        public void SetOriginalText(string txt)
        {
            originalText = txt;
        }

        private void OnDisable()
        {
            text.text = originalText;
        }
    }
}
