using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceForce : SteeringForce {
    private float m_feelerLength;
    private List<Obstacle> m_obstacles;

    public ObstacleAvoidanceForce() {
        m_feelerLength = 1f;
        m_obstacles = new List<Obstacle>();
    }

    public void SetFeelerLength(float length) {
        m_feelerLength = length;
    }

    public void AddObstacle(Obstacle obstacle) {
        m_obstacles.Add(obstacle);
    }
    
    public void ClearObstacles() {
        m_obstacles.Clear();
    }

    public override Vector2 GetForce(AIAgent agent) {
        Vector2 force = Vector2.zero;

        // Get the object
        Vector3 position = agent.gameObject.transform.position;

        // Get the velocity
        Vector2 velocity = Vector2.zero;
        agent.GetBlackboard().Get("Velocity", ref velocity);

        float ix = 0f;
        float iz = 0f;
        float t = 0f;

        // Are we moving
        float magSqr = velocity.sqrMagnitude;
        if (magSqr > 0) {
            // Loop through all obstacles
            foreach (var obstacle in m_obstacles) {
                if (obstacle.rayCircleIntersection(position.x, position.y, velocity.x, velocity.y, ix, iz, ref t)) {
                    if (t >= 0 && t <= m_feelerLength) {
                        force.x += (ix - obstacle.GetCenter().x) / obstacle.GetSize().capsuleSize.x;
                        force.y += (iz - obstacle.GetCenter().z) / obstacle.GetSize().capsuleSize.x;
                    }
                }

                // Rotate feeler 30 degrees
                float s = Mathf.Sin(3.14159f * 0.15f);
                float c = Mathf.Cos(3.14159f * 0.15f);
                if (obstacle.rayCircleIntersection(position.x, position.y, velocity.x * c - velocity.y * s, velocity.x * s + velocity.y * c, ix, iz, ref t)) {
                    if (t >= 0 && t <= m_feelerLength) {
                        force.x += (ix - obstacle.GetCenter().x) / obstacle.GetSize().capsuleSize.x;
                        force.y += (iz - obstacle.GetCenter().z) / obstacle.GetSize().capsuleSize.x;
                    }
                }

                // Rotate feeler -30 degrees
                s = Mathf.Sin(3.14159f * 0.15f);
                c = Mathf.Cos(3.14159f * 0.15f);
                if (obstacle.rayCircleIntersection(position.x, position.y, velocity.x * c - velocity.y * s, velocity.x * s + velocity.y * c, ix, iz, ref t)) {
                    if (t >= 0 && t <= m_feelerLength) {
                        force.x += (ix - obstacle.GetCenter().x) / obstacle.GetSize().capsuleSize.x;
                        force.y += (iz - obstacle.GetCenter().z) / obstacle.GetSize().capsuleSize.x;
                    }
                }
            }
        }

        // Normalise the force
        magSqr = force.sqrMagnitude;
        if (magSqr > 0) {
            force.Normalize();
        }

        float maxForce = 0f;
        agent.GetBlackboard().Get("MaxForce", ref maxForce);

        return new Vector2(force.x * maxForce, force.y * maxForce);
    }
}
