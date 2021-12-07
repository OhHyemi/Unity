using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BannerData
{
    public string key_image;
    public string url;
}
public class SliderBanner : MonoBehaviour
{
    public GameObject prefab_banner;
    public GameObject prefab_button;
    private ScrollRect scrollRect;
    private int curBannerIndex;
    private Banner[] banners;
    private BannerData[] datas;
    private BannerIndexButton[] indexButtons;

    public void Initialize(BannerData[] datas)
    {
        this.datas = datas;

        banners = new Banner[datas.Length];
        indexButtons = new BannerIndexButton[datas.Length];
        
        for (int i = 0; i < datas.Length; i++)
        {
            banners[i] = Instantiate(prefab_banner, transform).GetComponent<Banner>();
            banners[i].Set(i,Resources.Load<Sprite>(datas[i].key_image), ClickBanner);
            
            indexButtons[i] = Instantiate(prefab_button, transform).GetComponent<BannerIndexButton>();
        }
    }
    void ClickBanner(int index)
    {
        
    }

    void MoveBanner()
    {
        
    }
}
