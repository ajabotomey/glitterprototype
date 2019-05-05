using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBombControl : MonoBehaviour
{
    [SerializeField] private GameObject smokeBomb;
    [SerializeField] private float fireRate = 1.0f;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool smokebomb = Input.GetButtonDown("Smokebomb");

        if (smokebomb && elapsedTime >= fireRate) {
            Instantiate(smokeBomb, transform.position, Quaternion.identity);

            elapsedTime = 0.0f;
        }

        elapsedTime += Time.deltaTime;
    }
}
