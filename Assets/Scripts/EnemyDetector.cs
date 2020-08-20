using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public float radius = 5.0f;
    public float angleRange = 90f;
    private CircleCollider2D rangeCollider;
    [HideInInspector]
    public bool inRange = false;
    [HideInInspector]
    public bool isVisible = false;
    public Vector2 Direction;
    private Vector2 Target;
    private float Distance;
    private Vector2 fovLine1;
    private Vector2 fovLine2;
    // Start is called before the first frame update
    void Start()
    {
        fovLine1 = Quaternion.AngleAxis(angleRange/2f,transform.forward)*transform.up*radius;
        fovLine2 = Quaternion.AngleAxis(-angleRange/2f,transform.forward)*transform.up*radius;
        rangeCollider = gameObject.GetComponent<CircleCollider2D>();
        rangeCollider.enabled = true;
        rangeCollider.radius = radius;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 pos = other.gameObject.transform.position;
        Vector2 dir = ((Vector2)(pos-transform.position)).normalized;
        RaycastHit2D hit;
        if(hit = Physics2D.Raycast(transform.position,dir,radius,LayerMask.GetMask("Detectors")))
            isVisible = hit.collider.gameObject.tag == "Detector" && Vector2.Angle(transform.up,dir) <= angleRange/2;
            Target = hit.point;
            // print(Vector2.Angle(transform.up,dir));
        inRange = true;
        Direction = dir;

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        isVisible = false;
    }
    private void Update()
    {
        fovLine1 = Quaternion.AngleAxis(angleRange/2f,transform.forward)*transform.up*radius;
        fovLine2 = Quaternion.AngleAxis(-angleRange/2f,transform.forward)*transform.up*radius;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position,transform.up*radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position,fovLine1);
        Gizmos.DrawRay(transform.position,fovLine2);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,radius);
        if(inRange)
        {
            Gizmos.color = Color.red;
            // Gizmos.DrawLine(transform.position,transform.position+(Vector3)Direction*radius);
            Gizmos.DrawRay(transform.position,Direction*radius);
            if(isVisible)
            {
                Gizmos.color = Color.green;    
            }
            else
                Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position,(Vector3)Target);
        }
    }
}
