using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class CircleController : MonoBehaviour
    {
        [SerializeField] private GameObject circlePlane;
        public void Shrink(Vector3 scale,float time)
        {
            StartCoroutine(ShrinkInit(scale,time));
        }

        IEnumerator ShrinkInit(Vector3 scale, float time)
        {
            Vector3 currentScale = circlePlane.transform.localScale;
            float curTime = 0;
            while (curTime < time)
            {
                circlePlane.transform.localScale = Vector3.Lerp(currentScale,scale,curTime/time);
                curTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}