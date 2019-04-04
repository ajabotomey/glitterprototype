using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuardNav : MonoBehaviour
{
    enum FSMState { Patrol, Chase, Death }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] patrolPoints;
    private int patrolIndex = 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseRange;

    private FSMState currentState;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = FSMState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

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

                UpdateChase();
                break;
            case FSMState.Death:
                UpdateDeath();
                break;
        }
    }

    void UpdatePatrol() {
        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 1) {
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

        agent.destination = player.transform.position;
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
}
