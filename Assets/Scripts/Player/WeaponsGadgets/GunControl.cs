using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private GameObject bullet;

    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask targetMask;

    [Tooltip("Accuracy of the gun")][SerializeField] private int accuracy = 80;
    private int MAX_ACCURACY = 100;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // No accidentally firing the weapon while selecting a weapon
        if (InputController.instance.SelectWeapon())
            return;

        if (WeaponControl.instance.CurrentWeapon == WeaponControl.WeaponState.GUN) {
            FireGun();
        }

        elapsedTime += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (SettingsManager.Instance.IsAutoAimEnabled()) {
            var autoAimStrength = SettingsManager.Instance.GetAutoAimStrength();
            var direction = bulletSpawnPoint.transform.rotation * Vector2.up;
            var target = LookForEnemyWithThickRaycast(bulletSpawnPoint.transform.position, direction, autoAimStrength);

            if (target) {
                // Snap the entire rotation of the character to that 
                PlayerControl.instance.RotateTowardsTarget(target);
            }
        }
    }

    private void FireGun()
    {
        bool fireBullet = InputController.instance.FireWeapon();

        if (fireBullet && elapsedTime >= fireRate) {
            GameObject firedBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            firedBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 500f);

            elapsedTime = 0.0f;
        }
    }

    public Transform LookForEnemyWithThickRaycast(Vector2 startWorldPos, Vector2 direction, float visibilityThickness)
    {
        if (visibilityThickness == 0) return null; //aim assist disabled
 
        int[] castOrder = { 2, 1, 3, 0, 4 };
        int numberOfRays = castOrder.Length;

        // TODO: Add these values to Settings so players can customise
        const float minDistanceAway = 2.5f; //don't find results closer than this
        const float castDistance = 30f;
        const float flareOutAngle = 30f;
 
        Transform target = null;
        foreach (int i in castOrder)
        {
            Vector2 perpDirection = Vector2.Perpendicular(direction);
            float perpDistance = -visibilityThickness * 0.5f + i * visibilityThickness /(numberOfRays-1);
            Vector2 startPos = perpDirection * perpDistance + startWorldPos;
 
            float angleOffset = -flareOutAngle * 0.5f + i * flareOutAngle / (numberOfRays - 1);
            Vector2 flaredDirection = direction.Rotate(angleOffset);
 
            RaycastHit2D hit = Physics2D.Raycast(startPos, flaredDirection, castDistance, targetMask);
            Debug.DrawRay(startPos, flaredDirection * castDistance, Color.yellow, Time.deltaTime);
            if (hit && IsInTargetLayer(hit.collider.gameObject.layer))
            {
                //make sure it's in range
                float distanceAwaySqr = (hit.transform.position.toVector2() - startWorldPos).sqrMagnitude;
                if (distanceAwaySqr > minDistanceAway * minDistanceAway)
                {
                    Debug.DrawRay(startPos, direction * castDistance, Color.red, Time.deltaTime);
                    target = hit.transform;
                    return target;
                }
            }
        }
 
        return target;
    }

    private bool IsInTargetLayer(int layer)
    {
        var targetLayer = (int)Mathf.Log(targetMask.value, 2);

        return layer == targetLayer;
    }
}
