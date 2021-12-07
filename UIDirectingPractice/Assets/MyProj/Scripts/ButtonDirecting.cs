using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MyTween;
using UnityEngine.UI;

public class ButtonDirecting : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image img;
    public AnimationCurve ease;
    public Vector2 toSize;
    public float duration;
    private bool isTweening = false;
    private event Action onClick;

    private Vector2 fromSize;

    private Tween tw_down;
    private Tween tw_up;
    
    
    private void Awake()
    {
        fromSize = transform.localScale;
        tw_down = transform.Scale(toSize, duration).SetEase(ease).SetAutoKill(false);
        tw_up = transform.Scale(Vector3.one, duration).SetEase(ease).SetAutoKill(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isTweening)
        {
            return;
        }
        tw_down.Play();
        // StartCoroutine(CoAnimateScale());

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        tw_up.Play();
        if (onClick != null)
        {
            onClick();
        }
    }

    public void AddListener(Action onClick)
    {
        this.onClick += onClick;
    }

    // IEnumerator CoAnimateScale()
    // {
    //     isTweening = true;
    //     
    //     float time = 0;
    //     while (time < duration)
    //     {
    //         transform.localScale =Vector2.Lerp(fromSize, toSize, ease.Evaluate(time / duration));
    //
    //         time += Time.deltaTime;
    //         
    //         yield return null;
    //     }
    //     isTweening = false;
    //     transform.localScale = fromSize;
    // }

}
