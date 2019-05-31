using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance = null;

    [SerializeField]
    private Rigidbody2D rb;

    private float offset = -90.0f;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField] private EntityHealth health;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
        if (InputController.instance.isControllerActive()) {
            var angle = InputController.instance.Rotation();

            if (angle == 0) {
                rb.MoveRotation(0);
            } else {
                var angleWithOffset = angle - offset;
                rb.MoveRotation(angleWithOffset);
            }
        } else {
            Vector3 difference = Camera.main.ScreenToWorldPoint(InputController.instance.MousePosition()) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            float rotationWithOffset = rotation_z + offset;

            rb.MoveRotation(rotationWithOffset);
        }
    }

    public void RotateTowardsTarget(Transform target)
    {
        var dir = target.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var angleWithOffset = angle - offset;
        rb.MoveRotation(angleWithOffset);
    }

    void HandleMovement() {
        float moveHorizontal = InputController.instance.Horizontal();
        float moveVertical = InputController.instance.Vertical();

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
