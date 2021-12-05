using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyTween
{
    public static class TweenManager 
    {
        #region Move
        public static Tween Move(this Transform tr, Vector3 to, float duration)
        {
            var tweener = new Tweener<Transform, Vector3>();
            tweener.Initialize(tr.gameObject, tr, tr.position,to, duration,TweenMove);
            return tweener.Tween;
        }
        public static Tween MoveX(this Transform tr, float to, float duration)
        {
            var position = tr.position;
            return Move(tr, new Vector3(to, position.y, position.z), duration);
        }
        public static Tween MoveY(this Transform tr, float to, float duration)
        {
            var position = tr.position;
            return Move(tr, new Vector3(position.x, to, position.z), duration);
        }
        public static Tween MoveZ(this Transform tr, float to, float duration)
        {
            var position = tr.position;
            return Move(tr, new Vector3(position.x, position.y, to), duration);
        }
        
        #endregion

        #region Scale
        public static Tween Scale(this Transform tr, Vector3 to, float duration)
        {
            var tweener = new Tweener<Transform, Vector3>();
            tweener.Initialize(tr.gameObject, tr, tr.localScale,to, duration,TweenScale);
            return tweener.Tween;
        }
        public static Tween ScaleX(this Transform tr, float to, float duration)
        {
            var localScale = tr.localScale;
            return Scale(tr, new Vector3(to, localScale.y, localScale.z), duration);
        }
        public static Tween ScaleY(this Transform tr, float to, float duration)
        {
            var localScale = tr.localScale;
            return Scale(tr, new Vector3(localScale.x, to, localScale.z), duration);
        }
        public static Tween ScaleZ(this Transform tr, float to, float duration)
        {
            var localScale = tr.localScale;
            return Scale(tr, new Vector3(localScale.x, localScale.y, to), duration);
        }
        #endregion

        #region Rotate
        public static Tweener<Transform, Vector3> Rotate(this Transform tr, Quaternion to, float duration)
        {
            Tweener<Transform, Vector3> tweener = null;
            return tweener;
        }
        #endregion

        public static Tween SetEase(this Tween tween, AnimationCurve ease)
        {
            if (!tween.IsPlaying)
            {
                tween.Ease = ease;
            }
            else
            {
                Debug.Log("tween is playing");
            }
            return tween;
        }
        public static Tween Fade(this CanvasGroup canvasGroup, float to, float duration)
        {
            var tweener = new Tweener<CanvasGroup, float>();
            tweener.Initialize(canvasGroup.gameObject, canvasGroup, canvasGroup.alpha,to, duration,TweenAlpha);
            return tweener.Tween;
        }
        public static Tween Fade(this CanvasGroup canvasGroup, float from, float to, float duration)
        {
            var tweener = new Tweener<CanvasGroup, float>();
            tweener.Initialize(canvasGroup.gameObject, canvasGroup, from,to, duration,TweenAlpha);
            return tweener.Tween;
        }
        public static Tween Fade(this MaskableGraphic maskable, float to, float duration)
        {
            var tweener = new Tweener<MaskableGraphic, Color>();
            tweener.Initialize(maskable.gameObject, maskable, maskable.color,new Color(maskable.color.r, maskable.color.g, maskable.color.b, to), duration, TweenColor);
            return tweener.Tween;
        }

        public static Tween Color(this MaskableGraphic maskable, float to, float duration)
        {
            return Fade(maskable, to, duration);
        }
        static void TweenAlpha(CanvasGroup canvasGroup, float from, float to, float t)
        {
            canvasGroup.alpha =UnityEngine.Color.Lerp(new Color(1, 1, 1, from), new Color(1, 1, 1, to), t).a;
        }
        static void TweenColor(MaskableGraphic maskable, Color from, Color to, float t)
        {
            maskable.color =  UnityEngine.Color.Lerp(from, to, t);
        }
        static void TweenMove(Transform target, Vector3 from, Vector3 to, float t)
        {
            target.position = Vector3.Lerp(from, to, t);
        }
        static void TweenScale(Transform target, Vector3 from, Vector3 to, float t)
        {
            target.localScale = Vector3.Lerp(from, to, t);
        }
        
        public static Tween SetAutoKill(this Tween tween, bool auto)
        {
            tween.IsAutoKill = auto;
            return tween;
        }

        public static Tween SetOnEnd(this Tween tween, Action onEnd)
        {
            tween.OnEnd += onEnd;
            return tween;
        }
    }
    
}