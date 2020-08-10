using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TimeController : MonoBehaviour
{
    public TimeManager time_mgr;

    public float maxTime = 5f;
    private float currTime;
    private float currSize;
    public float minSize =3.0f;
    private float maxSize;
    public CinemachineVirtualCamera vcam;
    public float camera_size_inc_multiplier = 2.0f;
    public float camera_size_dec_multiplier = 2.0f;

    public float barDepleteMultiplier = 1f;
    public float barFillMultiplier = 2.5f;

    public TimeBar timeBar;

    [HideInInspector]
    public bool isSlow = false;

    private void Start()
    {
        currSize = vcam.m_Lens.OrthographicSize;
        maxSize = currSize;
        currTime = maxTime;
        timeBar.SetMaxTime(maxTime);
    }

    void Update()
    {
        if(currTime > 0 && Input.GetButtonDown("Jump"))
        {
            time_mgr.SlowDown();
            isSlow = true;
        }
        if(currTime <= 0 || Input.GetButtonUp("Jump"))
        {
            time_mgr.NormalTime();
            isSlow = false;
        }
    }
    private void FixedUpdate()
    {
        if(isSlow)
        {
            currSize -= Time.fixedUnscaledDeltaTime*camera_size_inc_multiplier;
            currTime = currTime - Time.fixedUnscaledDeltaTime*barDepleteMultiplier;
        }
        else
        {
            currSize += Time.fixedUnscaledDeltaTime*camera_size_dec_multiplier;
            currTime = currTime + Time.fixedUnscaledDeltaTime*barFillMultiplier;
        }
        currTime = Mathf.Clamp(currTime,0,maxTime);
        currSize = Mathf.Clamp(currSize,minSize,maxSize);
        vcam.m_Lens.OrthographicSize = currSize;
        timeBar.SetTimeVal(currTime);
    }
}
