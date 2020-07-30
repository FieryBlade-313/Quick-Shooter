using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;
    public float powerForce = 2f;

    public float maxRadius = 2f;
    public Camera cam;

    public BulletMarker bulletMarker;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
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
