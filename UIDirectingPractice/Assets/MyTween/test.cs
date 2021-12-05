using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTween;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve curve;
    void Start()
    {
        Tween tw = transform.Move(Vector3.one * 5, 10);
        tw.SetEase(curve);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
