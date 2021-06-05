using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class ScaleFadeHandler : FadeHandler
    {
        private Vector3? scale = null;


        public override void FadeIn(float fadeDelay)
        {
            gameObject.SetActive(true);
            
            if (!scale.HasValue) scale = transform.localScale;

            transform.localScale = Vector3.zero;

            transform.DOScale(scale.Value, 0.25f).SetDelay(fadeDelay);
        }

        public override void FadeOut()
        {
            
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => gameObject.SetActive(false));
            
        }
    }
}