using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage;
    
    void OnCollisionEnter2D(Collision2D collision) {
        rb.velocity = Vector2.zero;

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<EntityHealth>().ApplyDamage(damage);
            InputController.instance.SetRumble(1.0f);
        }

        Destroy(gameObject);
    }
}
