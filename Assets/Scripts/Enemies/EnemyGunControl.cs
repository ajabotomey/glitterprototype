using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunControl : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private float fireRate = 1.0f;

    [SerializeField]
    private GameObject bullet;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start() {
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        elapsedTime += Time.deltaTime;
    }

    public void Fire() {
        if (elapsedTime >= fireRate) {
            GameObject firedBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            firedBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 500.0f);

            elapsedTime = 0.0f;
        }
    }
}
