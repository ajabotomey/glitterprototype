using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeControl : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 10.0f;
    [SerializeField] private GameObject grenadeObject;
    [SerializeField] private float zSpeed = 0;
    [SerializeField] private LayerMask obstacleMask;

    private float elapsedTime;
    private float maxDistance = 5000;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponControl.instance.CurrentWeapon == WeaponControl.WeaponState.GRENADE) {
            bool launchNoise = Input.GetButtonDown("Fire1");

            // Check that we aren't throwing it onto an object. Also make sure we aren't throwing over obstacles as well
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var distance = Vector2.Distance(transform.position, mousePos);
            RaycastHit2D initialHit = Physics2D.Raycast(mousePos, Vector2.zero, maxDistance, obstacleMask);
            RaycastHit2D obstructionHit = Physics2D.Raycast(transform.position, transform.up, distance, obstacleMask);

            Debug.Log(distance);

            if (!initialHit && !obstructionHit) {
                if (launchNoise) {

                    // If it is a small distance, we might as well drop it at the position as opposed to throwing it
                    //if (distance <= 3.0f) {
                    //    Noisemaker noise = Instantiate(noisemaker, mousePos, Quaternion.identity).GetComponent<Noisemaker>();
                    //    noise.Landed();
                    //} else {
                    Grenade grenade = Instantiate(grenadeObject, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Grenade>();
                    grenade.Init(distance, mousePos);
                    //}
                }
            } else {
                // Change the cursor
            }
        }


        elapsedTime += Time.deltaTime;
    }
}
