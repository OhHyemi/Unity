using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class BannerScrollRect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private ScrollRect scrollRect;
    public ScrollRect ScrollRect
    {
        get
        {
            if (scrollRect == null)
            {
                scrollRect = GetComponent<ScrollRect>();
            }
            return scrollRect;
        }
    }

    private event Action onDown;
    public bool isOnDown { get; private set; }

    public void SetOnDown(Action onDown)
    {
        this.onDown += onDown;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isOnDown = true;
        onDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isOnDown = false;
        onDown?.Invoke();
    }
}
