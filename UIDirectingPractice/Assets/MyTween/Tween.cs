using System;
using UnityEngine;

namespace MyTween
{
    public class Tween<T1, T2>: MonoBehaviour
    {
        private T1 target;
        private T2 v_start;
        private T2 v_end;
        private float duration;
        public AnimationCurve ease;

        private int loop; // -1 : infinite

        private Action<T1, T2, T2, float> tweener;

        private Action onPlay;
        private Action onComplete;
        private Action onPause;
        private Action onEnd;
        
        private float time = 0;
        private bool isPlaying;
        
        public void Initialize (T1 target, T2 vEnd, float duration, Action<T1, T2, T2, float> tweener)
        {
            this.target = target;
            this.v_end = vEnd;
            this.duration = duration;
            this.tweener = tweener;

            Play();
        }

        public void Play()
        {
            isPlaying = true;

        }

        public void Pause()
        {
            
        }

        public void Stop()
        {
            
        }

        public void Resume()
        {
            
        }

        private void Update()
        {
            if (isPlaying == true)
            {
                var pos = v_start;
                if (time < duration)
                {
                    tweener(target, v_start, v_end, ease.Evaluate(time / duration));
                    time += Time.deltaTime;
                }
                else
                {
                    isPlaying = false;
                }
            }
         
        }

    }
}

