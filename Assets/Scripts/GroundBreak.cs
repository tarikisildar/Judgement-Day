using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class GroundBreak : MonoBehaviour
    {
        float speed = 50f; //how fast it shakes
        float amount = 0.01f; //how much it shakes
        [SerializeField] private ParticleSystem dust;
        [SerializeField] private GameObject foreground;
        public bool broke = false;
        private Vector3 pos;
        
        public IEnumerator Shake(float time)
        {
            broke = true;
            pos = transform.position;
            float cur = 0;
            dust.Play();
            dust.GetComponent<AudioSource>().Play();
            while (cur < time)
            {
                transform.position += new Vector3(Mathf.Sin(Time.time * speed) * amount,0,0);
                cur += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(FadeOutSound());

            transform.position = pos;
            Dip();
        }

        private void Dip()
        {
            foreground.transform.DOMove(foreground.transform.position + Vector3.down*1.6f, 1f);
            StartCoroutine(WaitAndPlayDust(0.5f, false));
            
            foreground.transform.DOMove(foreground.transform.position, 1f).SetDelay(5f).OnComplete(()=>broke=false);

        }

        IEnumerator FadeOutSound()
        {
            var time = 1f;
            var sound = dust.GetComponent<AudioSource>();
            while (time > 0f)
            {
                sound.volume = time;
                time -= Time.deltaTime;
                yield return null;
            }

            sound.volume = 1f;
            sound.Stop();
            
        }
        IEnumerator WaitAndPlayDust(float wTime, bool play)
        {
            yield return new WaitForSeconds(wTime);
            if(play) dust.Play();
            else
            {
                dust.Stop();
            }
        }

        


        public void Break()
        {
            if(broke) return;
            StartCoroutine(Shake(2f));
        }
        
    }
}