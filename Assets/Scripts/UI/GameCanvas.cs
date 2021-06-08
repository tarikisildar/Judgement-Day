using UnityEngine;

namespace UI
{
    public class GameCanvas : CanvasGeneric
    {
        [SerializeField] private GameObject InputCanvas;
        [SerializeField] private GameObject YouDiedCanvas;
        [SerializeField] private GameObject scoreBoard;
        [SerializeField] private GameObject scoreButton;
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

        public void ScoreBoardActive()
        {
            scoreBoard.GetComponent<FadeHandler>().FadeIn(0);
            scoreBoard.GetComponent<ScorePanel>().Initialize();
            scoreButton.GetComponent<FadeHandler>().FadeOut();

        }
        
        public void ScoreBoardDeactive()
        {
            scoreBoard.GetComponent<FadeHandler>().FadeOut();
            scoreButton.SetActive(true);
            scoreButton.GetComponent<FadeHandler>().FadeIn(0);



        }
        

        
    }
}