using System;
using MyTween;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    [SerializeField] private AnimationCurve ease;
    private Image img_icon;
    private Image img_cooltime;
    private Text txt_timer;
    private Timer timer;
    private int cooltime = 5;

    private void Awake()
    {
        foreach (var img in GetComponentsInChildren<Image>())
        {
            if (img.name == "Image_Spell")
            {
                img_icon = img;
            }
            else if (img.name == "Image_CoolTime")
            {
                img_cooltime = img;
            }
        }
        
        var btn = img_icon.gameObject.AddComponent<Button>();
        btn.onClick.AddListener(OnClickSpell);

        timer = GetComponent<Timer>();
        txt_timer = GetComponentInChildren<Text>();
    }
    
    //바깥에서 불러줘야해!
    public void Initialize(Sprite sprite, int cooltime, bool useAble = true)
    {
        img_icon.sprite = sprite;
        this.cooltime = cooltime;

        OnTimeChanged(useAble ? cooltime : 0);
    }
    
    void UseSpell()
    {
        img_cooltime.raycastTarget = true;
        timer.StartTimer(cooltime, OnTimeChanged);
    }
    void OnClickSpell()
    {
        //어디론가 보내서 스킬을 발동되도록 해야겠지 ?
        Debug.Log("아 스킬 발동ㅋ");
        ClickAnimation();//연출!
        UseSpell();
    }

    void ClickAnimation()
    {
        transform.Scale(Vector3.one, Vector3.one * 1.1f, 0.1f).SetEase(ease,true).Play();
    }
    void OnTimeChanged(float time)
    {
        UpdateCooltimeImage(time);
        UpdateTimerText(time);
    }
    void UpdateCooltimeImage(float time)
    {
        if (time <= 0)
        {
            img_cooltime.raycastTarget = false;
        }
        if (cooltime > 0)
        {
            img_cooltime.fillAmount = time / cooltime;
        }
        else
        {
            img_cooltime.fillAmount = 0;
        }
    }
    void UpdateTimerText(float time)
    {
        txt_timer.text = time <= 0 ? string.Empty :  txt_timer.text = Math.Ceiling(time).ToString();
    }
}
