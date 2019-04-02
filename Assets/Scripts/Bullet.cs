using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Awake() {
        
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
