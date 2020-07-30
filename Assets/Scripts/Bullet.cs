using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool launch = false;
    [HideInInspector]
    public float maxForce;
    [HideInInspector]
    public float maxRadius;
    [HideInInspector]
    public Camera cam;
    [HideInInspector]
    public BulletMarker bulletMarker;
    public ParticleSystem explosion;

    private Vector2 launchDir;

    private Rigidbody2D rb; 
    private void Start()
    {
        launchDir = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        launch = true;
    }
    private void OnMouseDrag()
    {
        launchDir = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void Update()
    {
        if(launch)
        {
            bulletMarker.DrawMarker(transform.position,new Vector3(launchDir.x,launchDir.y,0),maxRadius);
            if(Input.GetMouseButtonUp(0))
            {
                // print(transform.position+" "+launchDir);
                Vector2 pos = new Vector2(transform.position.x,transform.position.y);
                Vector2 res = (pos-launchDir).normalized;
                float dist = Mathf.Clamp(Vector2.Distance(pos,launchDir)/maxRadius,0,1);
                rb.velocity = Vector2.zero;
                rb.AddForce(res*maxForce*dist,ForceMode2D.Impulse);
                bulletMarker.ResetMarker();
                launch = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Bullet")
        {
            bulletMarker.ResetMarker();
            if(other.tag != "Boundary"){
                var exp =Instantiate(explosion,transform.position,Quaternion.identity);
                Destroy(exp,2f);
            }
            Destroy(gameObject);
        }
    }
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     bulletMarker.ResetMarker();
    //     if(other.gameObject.tag != "Boundary"){
    //         var exp =Instantiate(explosion,transform.position,Quaternion.identity);
    //         Destroy(exp,2f);
    //     }
    //     Destroy(gameObject);
    // }

    private void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(transform.position.x,transform.position.y,0);
        Vector3 mouse = new Vector3(launchDir.x,launchDir.y,0);
        Vector3 res = 2*(pos - mouse) + pos;
        Gizmos.color = Color.green;
        if(launch)
        {
            Gizmos.DrawLine(pos,mouse);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pos,res);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(pos,maxRadius);
            // print(mouse+" "+pos+" "+res);
        }
    }
}
