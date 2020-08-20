using System.Collections;
using UnityEngine;

public class TurretBehaviourDefinition : MonoBehaviour
{
    public enum State
    {
        patrol, chase, shoot, wSearch,
    }
    public float rotSpeed = 1f;
    public float chaseSpeed_modifier = 5f;
    private float thresholdAngle = 1f;
    private float shootRange = 5f;
    Vector2 desiredDir;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;
    public float powerForce = 2f;
    public float maxRadius = 2f;
    public Camera cam;
    public BulletMarker bulletMarker;

    private float nextShootTime;
    private float fireRate = 0.35f;

    private void Awake()
    {
        SetDesiredDirection();
    }

    public void SetDesiredDirection(){
        desiredDir = Random.insideUnitCircle;
    }

    public void Patrol(){
        //Patroling code
        float angle = Vector2.SignedAngle(transform.up,desiredDir);
        if(Mathf.Abs(angle)<thresholdAngle)
        {
            SetDesiredDirection();
        }
        else
            transform.Rotate(0,0,angle*rotSpeed*Time.deltaTime);
        // print("Patroling");
    }
    public void Chase(Vector2 Direction){
        //Chasing code
        float angle = Vector2.SignedAngle(transform.up,Direction);
        if(Mathf.Abs(angle)<shootRange)
        {
            if(Time.time > nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time+fireRate;
            }
        }
        transform.Rotate(0,0,angle*rotSpeed*chaseSpeed_modifier*Time.deltaTime);
        // print("Chasing");
    }
    public void Shoot(){
        //Shooting code
        // print("Shooting");
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        Bullet bul = bullet.GetComponent<Bullet>();
        bul.cam = cam;
        bul.maxForce = powerForce;
        bul.maxRadius = maxRadius;
        bul.bulletMarker = bulletMarker;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up*bulletForce,ForceMode2D.Impulse);
    }
}
