using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisemakerControl : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 10.0f;
    [SerializeField] private GameObject noisemaker;
    [SerializeField] private float force = 200f;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        bool launchNoise = Input.GetButtonDown("Noisemaker");

        if (launchNoise && elapsedTime >= fireRate) {
            GameObject noiseObj = Instantiate(noisemaker, bulletSpawnPoint.position, Quaternion.identity);
            noiseObj.GetComponent<Rigidbody2D>().AddForce(transform.up * force);

            elapsedTime = 0.0f;
        }

        elapsedTime += Time.deltaTime;
    }
}
