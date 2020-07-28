using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public TimeManager time_mgr;

    public float maxTime = 5f;
    private float currTime;

    public float barDepleteMultiplier = 1f;
    public float barFillMultiplier = 2.5f;

    public TimeBar timeBar;

    [HideInInspector]
    public bool isSlow = false;

    private void Start()
    {
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
            currTime = currTime - Time.fixedUnscaledDeltaTime*barDepleteMultiplier;
        }
        else
        {
            currTime = currTime + Time.fixedUnscaledDeltaTime*barFillMultiplier;
        }
        currTime = Mathf.Clamp(currTime,0,maxTime);
        timeBar.SetTimeVal(currTime);
    }
}
