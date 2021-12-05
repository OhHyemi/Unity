using System;
using UnityEngine;

namespace MyTween
{
    public class Tween : MonoBehaviour
    {
        private AnimationCurve ease;

        public AnimationCurve Ease
        {
            get
            {
                return ease ??= AnimationCurve.Linear(0, 0, 1, 1);
            }
            set
            {
                if (value != null)
                {
                    ease = value;
                }
            }
        }
        private int loop; // -1 : infinite
        private Func<AnimationCurve, bool> tweener;
        public event Action OnPlay;
        public event Action OnPause;
        public event Action OnStop;
        public event Action OnComplete;
        public event Action OnEnd;

        public bool IsPlaying { get; private set; }
        public bool IsEnd{ get; private set;}
        public bool IsAutoKill{ get;  set;}

        public void Set(Func<AnimationCurve, bool> tweener)
        {
            this.tweener = tweener;
            IsAutoKill = true;
        }
        public void Play()
        {
            IsPlaying = true;
            if (!IsAutoKill)
            {
                IsEnd = false;
            }
        }

        public void Pause()
        {
            IsPlaying = false;   
        }

        public void Stop()
        {
            IsPlaying = false;   
        }
        public void End()
        {
            IsPlaying = false;
            if (OnEnd != null) OnEnd();
            if (IsAutoKill)
            {
                Destroy(this);
            }
        }

        public void Complete()
        {
            IsPlaying = false;   
        }

        void Update()
        {
            if (IsPlaying && !IsEnd)
            {
                IsEnd = tweener(Ease);
            }

            if (IsEnd)
            {
                End();
            }
        }
    }
}


