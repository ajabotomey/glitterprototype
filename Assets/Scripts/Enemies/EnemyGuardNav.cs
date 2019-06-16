using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuardNav : MonoBehaviour
{
    enum FSMState { Patrol, Searching, Chase, RunAway, Investigate }

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
    [SerializeField] private float investigateChaseRange;
    [SerializeField] private float stopRange;
    [SerializeField] private float rotateSpeed;
    [Tooltip("When investigating, increase FOV range to this value")][SerializeField] private float fovRangeInvestigate;
    [Tooltip("How long does the FOV increase last for?")] [SerializeField] private float investigateTimer;

    private FSMState currentState;
    private int patrolIndex = 0;
    private Vector2 lastKnownPosition;
    private bool trackingPlayer;
    private float stopPointDistance = 1.0f;
    private Vector2 chosenDeathPoint;
    private float initialFOVRange;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        trackingPlayer = false;
        currentState = FSMState.Patrol;
        chosenDeathPoint = Vector2.zero;
        initialFOVRange = fov.ViewRadius;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(currentState);
    }

    void UpdateState(FSMState currentState)
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
                        currentState = FSMState.Searching;
                        Debug.Log("Looking for player");
                        break;
                    }

                    currentState = FSMState.Patrol;
                    break;
                }
                UpdateChase();
                break;
            case FSMState.Searching:
                UpdateSearch();
                break;
            case FSMState.Investigate:
                UpdateInvestigation();
                break;
            case FSMState.RunAway:
                UpdateDeath();
                break;
        }
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

    public void InvestigateSound(Vector2 position)
    {
        // Stop
        agent.isStopped = true;
        // Wait for a few seconds


        fov.ViewRadius = fovRangeInvestigate;
        currentState = FSMState.Investigate;
        RotateAgent(position);
        agent.isStopped = false; // Restart the nav mesh agent
        agent.destination = position;
    }


    #region Update State Methods
    void UpdatePatrol()
    {
        player.GetComponent<MaskControl>().BeingChased = false;

        if (fov.ViewRadius == fovRangeInvestigate) {
            fov.ViewRadius = initialFOVRange;
        }

        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < stopPointDistance) {
            if (patrolIndex + 1 < patrolPoints.Length) {
                patrolIndex++;
            }
            else {
                patrolIndex = 0;
            }
        }

        var pos = patrolPoints[patrolIndex].position;
        RotateAgent(pos);

        agent.destination = pos;
    }

    void UpdateChase()
    {
        player.GetComponent<MaskControl>().BeingChased = true;

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

    void UpdateDeath()
    {
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

    void UpdateSearch()
    {
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

    public void UpdateInvestigation()
    {
        if (elapsedTime > investigateTimer) {
            fov.ViewRadius = initialFOVRange;
            currentState = FSMState.Patrol;
            elapsedTime = 0;
            return;
        }

        // If we spot the player at all, go into chase state
        if (fov.FOVDetect())
        {
            currentState = FSMState.Chase;
            elapsedTime = 0;
            return;
        }

        // TODO: Need to figure out how to actually do this properly
        if (EnemyController.instance.CheckEnemyPositionToSound(transform.position)) {
            // Rotate 360 degrees
            transform.Rotate(0, 0, 6.0f * 10.0f * Time.deltaTime);
        } else {
            RotateAgent(EnemyController.instance.SoundPosition);
        }

        elapsedTime += Time.deltaTime;
    }
    #endregion
}
