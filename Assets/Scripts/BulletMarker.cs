﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMarker : MonoBehaviour
{
    private LineRenderer bulletMarker;

    public float maxWidth = 0.5f;
    public float minWidth = 0.1f;
    public float offset = 1f;

    public float n = 3f;

    public TargetMarker targetMarker;

    private void Start()
    {
        bulletMarker = gameObject.GetComponent<LineRenderer>();
        bulletMarker.startWidth = 0;
        bulletMarker.endWidth = 0;
    }

    public void DrawMarker(Vector3 bulletPos, Vector3 mousePos,float radius)
    {
        Vector3 dir = -1*Vector3.Normalize(bulletPos - mousePos);
        Vector3 offsetPos = dir*offset+bulletPos;
        bulletMarker.SetPosition(0,offsetPos);
        float pullFactor = Mathf.Lerp(0,1f,Vector3.Distance(bulletPos,mousePos)/radius);
        mousePos = Vector3.Lerp(offsetPos,bulletPos+dir*radius,pullFactor);
        bulletMarker.startWidth =  (maxWidth-minWidth)*Mathf.Exp(-Mathf.Pow(Vector3.Distance(offsetPos,mousePos),n)/(radius-offset))+minWidth;
        // print(offset + " " + Vector3.Distance(offsetPos,bulletPos));
        // print(bulletMarker.startWidth);
        bulletMarker.SetPosition(1,mousePos);
        targetMarker.DrawMarker(bulletPos,-1*dir,pullFactor);
    }

    public void ResetMarker()
    {
        bulletMarker.SetPosition(0,transform.position);
        bulletMarker.SetPosition(1,transform.position);
        bulletMarker.startWidth = 0;
        targetMarker.ResetMarker();
    }
}
