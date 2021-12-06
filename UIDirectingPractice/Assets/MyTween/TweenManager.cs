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
        public static Tween Scale(this Transform tr, Vector3 from, Vector3 to, float duration)
        {
            var tweener = new Tweener<Transform, Vector3>();
            tweener.Initialize(tr.gameObject, tr, from,to, duration,TweenScale);
            return tweener.Tween;
        }
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

        public static Tween SetEase(this Tween tween, AnimationCurve ease, bool pingpong = false)//ping pong: 다시 돌아오는 형태의 애니메이션
        {
            if (!tween.IsPlaying) //트윈이 플레이 중이지 않을 때 수정!
            {
                if (pingpong)//핑퐁을 한다면
                {
                    Keyframe[] keyframes = new Keyframe[ease.length];//애니메이션 커브의 키 프레임만큼 키프레임 배열 생성
                    for (int i = 0; i < ease.length; i++)
                    {
                        keyframes[i] = ease.keys[i];//일대일 매칭을 해서
                        keyframes[i].time = keyframes[i].time / 2;//시간 내에 돌아와야함. 반절은 to로 반절은 from로 가야하기때문에 2로 나누어 줌!
                    }
                    ease = new AnimationCurve(keyframes);//새로운 커브를 생성해 이전 이즈와 교체
                    ease.postWrapMode = WrapMode.PingPong;//핑퐁으로 모드를 변경!
                }
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