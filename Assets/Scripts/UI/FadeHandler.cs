using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class FadeHandler : MonoBehaviour
    {
        private CanvasGroup canvasGroup;




        public void FadeIn(float fadeDelay)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            gameObject.SetActive(true);

            canvasGroup.interactable = false;

            canvasGroup.DOFade(1F, 0.25F).SetEase(Ease.InSine).OnComplete(() => { canvasGroup.interactable = true; }).SetDelay(fadeDelay);
        }


        public void FadeOut()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;

            canvasGroup.DOFade(0F, 0.25F).SetEase(Ease.InSine).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}