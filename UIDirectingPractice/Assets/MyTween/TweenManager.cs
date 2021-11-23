using UnityEngine;
using UnityEngine.UI;

namespace MyTween
{
    public static class TweenManager 
    {
        public static Tween<Transform, Vector3> Move(this Transform tr, Vector3 to, float duration)
        {
            TweenVector3 tween = tr.gameObject.AddComponent<TweenVector3>();
            tween.Initialize(tr, to, duration,TweenMove);
            return tween;
        }
        
        public static void TweenMove(Transform target, Vector3 from, Vector3 to, float t)
        {
            target.position = Vector3.Lerp(from, to, t);
            Debug.Log(target.position);
        }
        public static Tween<Transform, Vector3> MoveX(this Transform tr, float to, float duration)
        {
            var position = tr.position;
            return Move(tr, new Vector3(to, position.y, position.z), duration);
        }
        public static Tween<Transform, Vector3> MoveY(this Transform tr, float to, float duration)
        {
            var position = tr.position;
            return Move(tr, new Vector3(position.x, to, position.z), duration);
        }
        public static Tween<Transform, Vector3> MoveZ(this Transform tr, float to, float duration)
        {
            var position = tr.position;
            return Move(tr, new Vector3(position.x, position.y, to), duration);
        }

        // static IEnumerator CoTransformMoveTween(Transform tr, Vector3 to, float duration, AnimationCurve ease)
        // {
        //     float time = 0;
        //     while (time < duration)
        //     {
        //         tr.position = Vector3.Slerp(tr.position, to, ease.Evaluate(time / duration));
        //        time += Time.deltaTime;
        //     
        //         yield return null;
        //     }
        // }
        //
        
        
        public static Tween<Transform, Vector3> Scale(this Transform tr, Vector3 to, float duration)
        {
            Tween<Transform, Vector3> tween = tr.gameObject.AddComponent<Tween<Transform, Vector3>>();
            return tween;
        }
        public static Tween<Transform, Vector3> ScaleX(this Transform tr, float to, float duration)
        {
            Tween<Transform, Vector3> tween = null;
            return tween;
        }
        public static Tween<Transform, Vector3> ScaleY(this Transform tr, float to, float duration)
        {
            Tween<Transform, Vector3> tween = null;
            return tween;
        }
        public static Tween<Transform, Vector3> ScaleZ(this Transform tr, float to, float duration)
        {
            Tween<Transform, Vector3> tween = null;
            return tween;
        }

        public static Tween<Transform, Vector3> Rotate(this Transform tr, Quaternion to, float duration)
        {
            Tween<Transform, Vector3> tween = null;
            return tween;
        }
        
        public static Tween<Transform, Vector3> SetEase(this Tween<Transform, Vector3> tween, AnimationCurve ease)
        {
            tween.ease = ease;
            return tween;
        }
        public static void Fade(this MaskableGraphic maskable, float to, float duration)
        {

        }
        
        
    }
}