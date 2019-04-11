using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private float offset = -90.0f;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField] private EntityHealth health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    void FixedUpdate() {
        RotateCharacter();
        HandleMovement();
    }

    void RotateCharacter() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        float rotationWithOffset = rotation_z + offset;

        rb.MoveRotation(rotationWithOffset);
    }

    void HandleMovement() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //gameObject.transform.position = new Vector2(transform.position.x + (moveHorizontal * speed), transform.position.y + (moveVertical * speed));
        float posX = transform.position.x + moveHorizontal * speed * Time.fixedDeltaTime;
        float posY = transform.position.y + moveVertical * speed * Time.fixedDeltaTime;

        rb.MovePosition(new Vector2(posX, posY));
    }

    void CheckHealth() {
        if (health.isDead()) {
            UIController.instance.Died();
        }
    }
}
