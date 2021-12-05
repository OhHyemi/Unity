using System;
using MyTween;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ButtonCanvasGroup : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            return canvasGroup;
        }
    }

    public AnimationCurve curve;

    public bool IsOpen;

    private Tween tw_open;
    private Tween tw_close;

    private void Start()
    {
        IsOpen = true;
        tw_open = CanvasGroup.Fade(0, 1,0.25f).SetAutoKill(false).SetEase(curve);
        tw_close = CanvasGroup.Fade(1, 0,0.25f).SetAutoKill(false).SetEase(curve).SetOnEnd(() => { IsOpen = false;});
    }

    public void Open()
    {
        tw_open.Play();
        CanvasGroup.blocksRaycasts = true;
        IsOpen = true;
    }

    public void Close()
    {
        tw_close.Play();
        CanvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Open();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Close();
        }
        
    }
}
