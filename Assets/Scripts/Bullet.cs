using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage;

    void Awake() {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        rb.velocity = Vector2.zero;

        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<EnemyHealth>().ApplyDamage(damage);
        }


        Destroy(gameObject);
    }
}
