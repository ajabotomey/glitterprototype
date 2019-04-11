﻿using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private float fireRate = 1.0f;

    [SerializeField]
    private GameObject bullet;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //bool fireBullet = Input.GetButtonDown("Fire1");
        bool fireBullet = Input.GetButton("Fire1");

        if (fireBullet && elapsedTime >= fireRate) {
            GameObject firedBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            firedBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 500.0f);

            elapsedTime = 0.0f;
        }

        elapsedTime += Time.deltaTime;
    }
}
