using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public float radius = 5.0f;
    private CircleCollider2D rangeCollider;
    private bool inRange = false;
    private bool isVisible = false;
    private Vector2 Direction;
    private Vector2 Target;
    private float Distance;
    // Start is called before the first frame update
    void Start()
    {
        rangeCollider = gameObject.GetComponent<CircleCollider2D>();
        rangeCollider.enabled = true;
        rangeCollider.radius = radius*0.2f;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 pos = other.gameObject.transform.position;
        Vector2 dir = ((Vector2)(pos-transform.position)).normalized;
        RaycastHit2D hit;
        if(hit = Physics2D.Raycast(transform.position,dir,radius,LayerMask.GetMask("Detectors")))
            isVisible = hit.collider.gameObject.tag == "Detector";
            Target = hit.point;
        inRange = true;
        Direction = dir;
        print("Is Visible : "+" "+isVisible);

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        isVisible = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,radius);
        if(inRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,transform.position+(Vector3)Direction*radius);
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
