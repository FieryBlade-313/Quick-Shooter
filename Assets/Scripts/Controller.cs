using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    public float speed = 10f;

    private Vector2 AxisInput = new Vector2(0,0);
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        AxisInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        // print(AxisInput);
        // print(rb.velocity);
        rb.velocity = new Vector3(AxisInput[0],AxisInput[1],0);
    }
}
