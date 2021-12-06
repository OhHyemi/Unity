using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Coroutine curTimer;
    
    public void StartTimer(int time, Action<float> onValueChanged)
    {
        //진행되던 타이머가 있다면 멈추고 새로 시작
        if (curTimer != null)
        {
            StopCoroutine(curTimer);
        }
        curTimer = StartCoroutine(CoStartTimer(time, onValueChanged));
    }
    //타이머를 진행할 시간, 시간이 바뀔때마다 어떠한 행동을할건지
    IEnumerator CoStartTimer(int time, Action<float> onValueChanged)
    {
        float timer = 0;
        while (true)
        {
            //타이머가 지정된 시간을 넘기면 break!
            if (timer >= time)
            {
                break;
            }
            //시간을 더해주자!
            timer += Time.deltaTime;
            yield return null;
            //남은 시간을 보내주기
            onValueChanged(time - timer);
        }
        //타이머가 끝나면 코루틴을 null
        curTimer = null;
    }
}
