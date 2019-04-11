using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuardNav : MonoBehaviour
{
    enum FSMState { Patrol, Looking, Chase, Death }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] patrolPoints;
    private int patrolIndex = 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseRange;
    [SerializeField] private float stopRange;

    private FSMState currentState;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private EnemyHealth health;

    private Vector2 lastKnownPosition;
    private bool trackingPlayer;
    private float stopPointDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        trackingPlayer = false;
        currentState = FSMState.Patrol;
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
                }

                UpdatePatrol();
                break;
            case FSMState.Chase:
                if (distanceToPlayer > chaseRange) {
                    currentState = FSMState.Patrol;
                }

                if (!fov.FOVDetect()) {
                    currentState = FSMState.Patrol;
                } else if (trackingPlayer) {
                    currentState = FSMState.Looking;
                    Debug.Log("Looking for player");
                }

                UpdateChase();
                break;
            case FSMState.Looking:
                LostPlayer();
                break;
            case FSMState.Death:
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
        RotateAgent(player.transform.position);

        if (Vector2.Distance(transform.position, player.transform.position) < stopRange) {
            agent.isStopped = true; ;
        }

        trackingPlayer = true;
        lastKnownPosition = player.transform.position;

        agent.destination = lastKnownPosition;
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
            currentState = FSMState.Death;
        }
    }

    void UpdateDeath() {
        Destroy(this.gameObject);
    }

    void LostPlayer() {
        if (Vector2.Distance(transform.position, lastKnownPosition) < stopPointDistance) {
            currentState = FSMState.Patrol;
            Debug.Log("Back to patrol");
        }

        if (fov.FOVDetect()) {
            currentState = FSMState.Chase;
            Debug.Log("Back to the chase");
        }

        agent.destination = lastKnownPosition;
    }
}
