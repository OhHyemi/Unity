using System;
using UnityEngine;

namespace MyTween
{
    public class Tweener<T1, T2>
    {
        public Tween Tween { get; private set; }
        
        private T1 target;
        private T2 v_start;
        private T2 v_end;
        private float duration;

        private Action<T1, T2, T2, float> tweener;

        private float time;

        public void Initialize (GameObject gameObject, T1 target, T2 v_start, T2 v_end, float duration, Action<T1, T2, T2, float> tweener)
        {
            this.target = target;
            this.v_start = v_start;
            this.v_end = v_end;
            this.duration = duration;
            this.tweener = tweener;
            Tween = gameObject.AddComponent<Tween>();
            Tween.Set(Tweening);
        }

        private bool Tweening(AnimationCurve ease)
        {
            if (time < duration)
            {
                tweener(target, v_start, v_end, ease.Evaluate(time / duration));
                time += Time.deltaTime;
                return false;
            }
            else
            {
                time = 0;
                return true;
            }
       
        }
    }
}

