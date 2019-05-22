using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform laserHit;

    private int maxDistance = 5000;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, maxDistance, targetMask | obstacleMask);

        lineRenderer.SetPosition(0, transform.position);

        if (hit) {
            if (hit.collider) {
                laserHit.position = hit.point;
                lineRenderer.SetPosition(1, laserHit.position);
            }
        } else {
            lineRenderer.SetPosition(1, new Vector3(0, maxDistance, 0));
        }
    }
}
