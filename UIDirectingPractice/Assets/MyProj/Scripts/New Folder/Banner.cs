using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banner : MonoBehaviour
{
    private int index;
    private Image img_banner;
    private event Action<int> onClick;

    public void Set(int index, Sprite sprite_banner, Action<int> onClick)
    {
        this.index = index;
        this.img_banner.sprite = sprite_banner;
        this.onClick += onClick;
    }

    public void OnClickBanner()
    {
        onClick?.Invoke(index);
    }

}
