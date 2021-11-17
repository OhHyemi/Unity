using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyTween
{
    public static class Tween
    {
        public static event Action onPlay;
        public static event Action onPause;
        public static event Action onEnd;
        public static Transform Move(this Transform tr, Vector3 to, float duration, AnimationCurve ease)
        {

            return tr;
        }
        public static Transform MoveX(this Transform tr, float to, float duration, AnimationCurve ease)
        {
            return tr;
        }
        public static Transform MoveY(this Transform tr, float to, float duration, AnimationCurve ease)
        {
            return tr;
        }
        public static Transform MoveZ(this Transform tr, float to, float duration, AnimationCurve ease)
        {
            return tr;
        }

        static IEnumerator CoTransformMoveTween(Transform tr, Vector3 to, float duration, AnimationCurve ease)
        {
            float time = 0;
            while (time < duration)
            {
                tr.position = Vector3.Slerp(tr.position, to, ease.Evaluate(time / duration));
               time += Time.deltaTime;
            
                yield return null;
            }
        }
        
        
        
        public static void Scale(this Transform tr, Vector3 to, float duration)
        {
            
        }
        public static void ScaleX(this Transform tr, float to, float duration)
        {
            
        }
        public static void ScaleY(this Transform tr, float to, float duration)
        {
            
        }
        public static void ScaleZ(this Transform tr, float to, float duration)
        {
            
        }

        public static void Rotate(this Transform tr, Quaternion to, float duration)
        {
            
        }
        public static void Fade(this MaskableGraphic maskable, float to, float duration)
        {

        }
    }
}

