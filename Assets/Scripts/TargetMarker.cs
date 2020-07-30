using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMarker : MonoBehaviour
{    
    private LineRenderer targetMarker;
    public float width = 0.5f;
    public float height = 0.3f;
    public float offset = 0.2f;
    public Gradient colorBar;

    private void Start()
    {
        targetMarker = gameObject.GetComponent<LineRenderer>();
        targetMarker.startWidth = 0;
        targetMarker.endWidth = 0;
    }

    public void DrawMarker(Vector3 bulletPos, Vector3 dirVec,float pullFactor)
    {
        Vector3 offsetPos = dirVec*offset + bulletPos;
        Vector3 endPos = dirVec*(height) + offsetPos;
        targetMarker.SetPosition(0,offsetPos);
        targetMarker.startWidth = width;
        targetMarker.SetPosition(1,endPos);
        targetMarker.startColor = colorBar.Evaluate(pullFactor);
        targetMarker.endColor = colorBar.Evaluate(pullFactor);
    }
    public void ResetMarker()
    {
        targetMarker.SetPosition(0,transform.position);
        targetMarker.SetPosition(1,transform.position);
        targetMarker.startWidth = 0;
    }
}
