using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource source;
    [SerializeField] private float soundRadius;

    private bool deployed = false;

    // Update is called once per frame
    void Update()
    {
        if (!deployed) {
            if (rb.velocity == Vector2.zero) {
                if (source.isPlaying) {
                    source.Stop();
                }
                source.Play();
                EnemyController.instance.ReactToSound(soundRadius, transform.position);
                deployed = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;

        if (collision.gameObject.tag == "Enemy") {
            Destroy(this.gameObject);
        }
    }
}
