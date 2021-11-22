using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyTween
{
    public class Tween<T1, T2>
    {
        private T1 target;
        private T2 v_start;
        private T2 v_end;
        private AnimationCurve ease;

        private int loop; // -1 : infinite

        private Action onPlay;
        private Action onComplete;
        private Action onPause;
        private Action onEnd;

        public void Play()
        {
            
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
        
        

    }
}

