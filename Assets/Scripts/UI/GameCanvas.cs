using UnityEngine;

namespace UI
{
    public class GameCanvas : CanvasGeneric
    {
        [SerializeField] private GameObject InputCanvas;
        [SerializeField] private GameObject YouDiedCanvas;
        public override void Initialize()
        {
            base.Initialize();
        }

        public void TurnInput(bool on)
        {
            if (!on)
            {
                InputCanvas.GetComponent<FadeHandler>().FadeOut();
            }
            else
            {
                InputCanvas.GetComponent<FadeHandler>().FadeIn(0.5f);

            }
        }

        public void YouDiedActivate(float time)
        {
            YouDiedCanvas.GetComponent<YouDiedUI>().Initialize(time);
        }

        public void YouDiedDeactivate()
        {
            YouDiedCanvas.GetComponent<FadeHandler>().FadeOut();
        }
        

        
    }
}