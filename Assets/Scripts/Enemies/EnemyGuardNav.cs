using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuardNav : MonoBehaviour
{
    enum FSMState { Patrol, Looking, Chase, RunAway }

    [Header("Enemy Components")]
    [SerializeField] private FieldOfView fov;
    [SerializeField] private EntityHealth health;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyGunControl gunControl;

    [Header("Enemy Data")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseRange;
    [SerializeField] private float stopRange;
    [SerializeField] private float rotateSpeed;

    private FSMState currentState;
    private int patrolIndex = 0;
    private Vector2 lastKnownPosition;
    private bool trackingPlayer;
    private float stopPointDistance = 1.0f;
    private Vector2 chosenDeathPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        trackingPlayer = false;
        currentState = FSMState.Patrol;
        chosenDeathPoint = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        agent.isStopped = false;

        switch (currentState) {
            case FSMState.Patrol:
                if (fov.FOVDetect()) {
                    currentState = FSMState.Chase;
                    break;
                }

                UpdatePatrol();
                break;
            case FSMState.Chase:
                if (distanceToPlayer > chaseRange) {
                    currentState = FSMState.Patrol;
                    break;
                }

                if (!fov.FOVDetect()) {
                    if (trackingPlayer) {
                        currentState = FSMState.Looking;
                        Debug.Log("Looking for player");
                        break;
                    }

                    currentState = FSMState.Patrol;
                    break;
                }
                UpdateChase();
                break;
            case FSMState.Looking:
                LostPlayer();
                break;
            case FSMState.RunAway:
                UpdateDeath();
                break;
        }
    }

    void UpdatePatrol() {
        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < stopPointDistance) {
            if (patrolIndex + 1 < patrolPoints.Length) {
                patrolIndex++;
            } else {
                patrolIndex = 0;
            }
        }

        var pos = patrolPoints[patrolIndex].position;
        RotateAgent(pos);

        agent.destination = pos;
    }

    void UpdateChase() {
        // Move towards player
        RotateAgent(player.transform.position);

        if (Vector2.Distance(transform.position, player.transform.position) < stopRange) {
            agent.isStopped = true;
        }

        trackingPlayer = true;
        lastKnownPosition = player.transform.position;

        agent.destination = lastKnownPosition;

        // Fire at player
        gunControl.Fire();
    }

    void RotateAgent(Vector3 position) {
        CheckDeath();

        var step = rotateSpeed * Time.deltaTime;

        var patrolPointDir = position - transform.position;
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, step);
    }

    void CheckDeath() {
        if (health.isDead()) {
            currentState = FSMState.RunAway;
        }
    }

    void UpdateDeath() {
        //Destroy(this.gameObject);
        if (chosenDeathPoint == Vector2.zero) {
            chosenDeathPoint = EnemyController.instance.chooseRandomDeathPoint();
        }

        RotateAgent(chosenDeathPoint);

        if (Vector2.Distance(transform.position, chosenDeathPoint) < stopPointDistance) {
            Destroy(gameObject);
            return;
        }

        agent.destination = chosenDeathPoint;
    }

    void LostPlayer() {
        if (Vector2.Distance(transform.position, lastKnownPosition) < stopPointDistance) {
            trackingPlayer = false;
            currentState = FSMState.Patrol;
            Debug.Log("Back to patrol");
            return;
        }

        if (fov.FOVDetect()) {
            currentState = FSMState.Chase;
            Debug.Log("Back to the chase");
            return;
        }

        RotateAgent(lastKnownPosition);

        agent.destination = lastKnownPosition;
    }
}
