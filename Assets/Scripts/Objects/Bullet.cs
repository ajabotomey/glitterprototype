using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private int damage;

    public void Init(int newDamage)
    {
        damage = newDamage;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        rb.velocity = Vector2.zero;

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<EntityHealth>().ApplyDamage(damage);
            InputController.instance.SetRumble(1.0f);
        }

        Destroy(gameObject);
    }
}
