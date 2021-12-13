using System.Collections;
using UnityEngine;

public struct BannerData
{
    public string key_image;
    public string url;
}

public class SliderBanner : MonoBehaviour
{
    public Transform tr_buttons;
    public GameObject prefab_banner;
    public GameObject prefab_button;
    public AnimationCurve ease;

    private BannerScrollRect bannerScrollRect;

    private BannerScrollRect BannerScrollRect
    {
        get
        {
            if (bannerScrollRect == null)
            {
                bannerScrollRect = GetComponentInChildren<BannerScrollRect>();
            }

            return bannerScrollRect;
        }
    }

    private int curBannerIndex;
    private Banner[] banners;
    private BannerData[] datas = new BannerData[5]; //test로 갯수 정해줌 ㅎㅎ
    private BannerIndexButton[] indexButtons;
    private float[] points;
    private float duration = 0.25f;

    private Coroutine moveCoroutine;

    private bool isBannerMoving;

    #region Snap Field

    public float snapValue = 0.3f;//banner의 얼마만큼 왔을 때 넘어갈 것인지의 값(0~1)
    private float snapRange;//계산된 범위

    #endregion

    #region AutoMove Field

    private bool isAbleAutoMove;
    private float autoTimer;//자동으로 움직이는 banner가 작동하는 간격을 체크하는 타이머
    private float autoMoveTimer;//자동으로 움직이는 banner 기능에서 사용하는 타이머
    private float hnp;//horizontal nomalized position - 자동무브에서 사용할 스크롤의 정규화한 x 포지션
    private int autoNextIndex;//다음으로 올 banner index
    public float duration_stop;//자동으로 banner바뀐후 멈춰있는 시간

    #endregion


    private void Start()
    {
        Initialize(datas);
    }

    public void Initialize(BannerData[] datas)
    {
        var perPoint = (float) 1 / (datas.Length - 1);
        snapRange = perPoint * snapValue;
        this.datas = datas;

        banners = new Banner[datas.Length];
        indexButtons = new BannerIndexButton[datas.Length];

        points = new float[datas.Length];

        for (int i = 0; i < datas.Length; i++)
        {
            datas[i].key_image = "Common_AD_01"; //test
            banners[i] = Instantiate(prefab_banner, BannerScrollRect.ScrollRect.content).GetComponent<Banner>();
            banners[i].Initialize(i, Resources.Load<Sprite>(datas[i].key_image), ClickBanner);

            indexButtons[i] = Instantiate(prefab_button, tr_buttons).GetComponent<BannerIndexButton>();
            indexButtons[i].Initialize(i, MoveBannerByIndex, false);

            points[i] = perPoint * i;
        }

        indexButtons[0].SelectWithoutNotify(true);
        BannerScrollRect.SetOnDown(() => { isAbleAutoMove = false; });
        BannerScrollRect.ScrollRect.onValueChanged.AddListener(ScrollSnap);


        hnp = BannerScrollRect.ScrollRect.horizontalNormalizedPosition;
        autoNextIndex = 1;
        isAbleAutoMove = true;
        // autoMoveCoroutine = StartCoroutine(CoAutoMoveBanner());
    }

    private void Update()
    {
        if (isAbleAutoMove)
        {
            AutoMoveBanner();
        }
    }

    void ScrollSnap(Vector2 value) //snap
    {
        //유저가 스크롤을 조작하고 있는 경우 or 오토무브가 가능한 경우 or 배너가 움직이는 중일 경우(버튼 조작으로) 
        if (BannerScrollRect.isOnDown || isAbleAutoMove || isBannerMoving)
        {
            return;
        }

        if (value.x > points[curBannerIndex]) //오른쪽 snap
        {
            if (value.x >= points[curBannerIndex] + snapRange)
            {
                var nextIndex = curBannerIndex + 1 >= datas.Length ? 0 : curBannerIndex + 1;
                MoveBannerByIndex(nextIndex);
            }
            else
            {
                MoveBannerByIndex(curBannerIndex);
            }
        }
        else //왼쪽 snap
        {
            if (value.x <= points[curBannerIndex] - snapRange)
            {
                var prevIndex = curBannerIndex - 1 < 0 ? 0 : curBannerIndex - 1;
                MoveBannerByIndex(prevIndex);
            }
            else
            {
                MoveBannerByIndex(curBannerIndex);
            }
        }
    }

    void ClickBanner(int index)
    {
        //이동이라던가 출력이라던가.. 넣어주기
    }

    void MoveBannerByIndex(int index)
    {
        //해당 인덱스 배너로 무브~ 
        isAbleAutoMove = false;
        isBannerMoving = true;
        if (curBannerIndex != index)
        {
            indexButtons[curBannerIndex].SelectWithoutNotify(false);
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        StartCoroutine(CoMoveBanner(index));
    }

    IEnumerator CoMoveBanner(int index)
    {
        curBannerIndex = index;
        indexButtons[curBannerIndex].SelectWithoutNotify(true);
        
        var timer = 0f;
        var startPoint = BannerScrollRect.ScrollRect.horizontalNormalizedPosition;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            //스크롤 포인트 조절! lerp를 사용하여 부드럽게 이동시키자!
            BannerScrollRect.ScrollRect.horizontalNormalizedPosition =
                Mathf.Lerp(startPoint, points[index], ease.Evaluate(timer / duration));
            yield return null;
        }

        isAbleAutoMove = true;
        isBannerMoving = false;
        autoTimer = 0; //무브를 조작했다면 오토무브 타이머를 초기화 
    }

    void AutoMoveBanner() //자동으로 움직이는 기능
    {
        if (autoTimer < duration_stop)
        {
            if (autoMoveTimer < duration)
            {
                autoMoveTimer += Time.deltaTime;
                //스크롤 포인트 조절! lerp를 사용하여 부드럽게 이동시키자!
                BannerScrollRect.ScrollRect.horizontalNormalizedPosition = Mathf.Lerp(hnp, points[curBannerIndex],
                    ease.Evaluate(autoMoveTimer / duration));
            }

            autoTimer += Time.deltaTime;
        }
        else
        {
            autoTimer = 0;
            autoMoveTimer = 0;
            hnp = BannerScrollRect.ScrollRect.horizontalNormalizedPosition;

            indexButtons[curBannerIndex].SelectWithoutNotify(false);

            autoNextIndex = curBannerIndex + 1;
            curBannerIndex = autoNextIndex >= datas.Length ? 0 : autoNextIndex;
            indexButtons[curBannerIndex].SelectWithoutNotify(true);
        }
    }
}