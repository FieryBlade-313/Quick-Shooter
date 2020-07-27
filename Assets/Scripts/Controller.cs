using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    public float speed = 50f;
    public float angleSmoothSpeed = 10f;
    public Camera cam;
    private Vector2 AxisInput = new Vector2(0,0);
    private Vector2 mousePos = new Vector2(0,0);
    private Rigidbody2D rb;

    private TimeController time_ctr;
    // Start is called before the first frame update
    void Start()
    {
        time_ctr = gameObject.GetComponent<TimeController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AxisInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // print(AxisInput);
        // print(rb.velocity);
        // print(shoot);
        // print(transform.rotation);
        Vector2 lookDir = mousePos -  rb.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.MovePosition(rb.position+AxisInput*speed*Time.fixedDeltaTime);
        rb.rotation = angle;
    }
}
