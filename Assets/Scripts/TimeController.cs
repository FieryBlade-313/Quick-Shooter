using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public TimeManager time_mgr;

    [HideInInspector]
    public bool isSlow = false;
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            time_mgr.SlowDown();
            isSlow = true;
        }
        if(Input.GetButtonUp("Jump"))
        {
            time_mgr.NormalTime();
            isSlow = false;
        }
    }
}
