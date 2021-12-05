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
    public Vector2 toSize = Vector2.one;
    public float duration;
    private bool isTweening = false;
    private event Action onClick;

    private Vector2 fromSize;
    
    private void Awake()
    {
        fromSize = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isTweening)
        {
            return;
        }
        // StartCoroutine(CoAnimateScale());

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        var tween = transform.Scale(toSize, duration).SetEase(ease);
        tween.Play();
        // if (onClick != null)
        // {
        //     onClick();
        // }
    }

    public void AddListener(Action onClick)
    {
        this.onClick += onClick;
    }

    IEnumerator CoAnimateScale()
    {
        isTweening = true;
        
        float time = 0;
        while (time < duration)
        {
            transform.localScale =Vector2.Lerp(fromSize, toSize, ease.Evaluate(time / duration));

            time += Time.deltaTime;
            
            yield return null;
        }
        isTweening = false;
        transform.localScale = fromSize;
    }

}
