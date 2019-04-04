using UnityEngine;

public struct WanderData {
    public float offset;
    public float radius;
    public float jitter;
    public float x;
    public float y;
}

public class WanderForce : SteeringForce {
    public override Vector2 GetForce(AIAgent agent) {
        WanderData wd = new WanderData();
        if (agent.GetBlackboard().Get("WanderData", ref wd) == false) {
            return Vector2.zero;
        }

        //Vector2 jitterOffset = AIUtilites.CircularRand(wd.jitter);
        //Vector2 jitterOffset = Random.insideUnitCircle * wd.jitter;
        Vector2 jitterOffset = Random.insideUnitCircle.normalized * wd.jitter;
        
        // Applied Jitter
        Vector2 wander = new Vector2(wd.x, wd.y);
        wander.x += jitterOffset.x;
        wander.y += jitterOffset.y;

        float magnitude = Mathf.Sqrt(wander.x * wander.x + wander.y * wander.y);

        // Applied Radius
        wander.x = (wander.x / magnitude) * wd.radius;
        wander.y = (wander.y / magnitude) * wd.radius;

        wd.x = wander.x;
        wd.y = wander.y;

        // Get Target Velocity
        Vector2 velocity = Vector2.zero;
        agent.GetBlackboard().Get("Velocity", ref velocity);
        float vx = velocity.x;
        float vy = velocity.y;

        magnitude = vx * vx + vy * vy;
        if (magnitude > 0) {
            magnitude = Mathf.Sqrt(magnitude);
            vx /= magnitude;
            vy /= magnitude;
        }

        wander.x += vx * wd.offset;
        wander.y += vy * wd.offset;

        // Normalise wander direction
        magnitude = wander.x * wander.x + wander.y * wander.y;
        if (magnitude > 0) {
            magnitude = Mathf.Sqrt(magnitude);
            wander.x /= magnitude;
            wander.y /= magnitude;
        }

        float maxForce = 0;
        agent.GetBlackboard().Get("MaxForce", ref maxForce);

        return new Vector2(wander.x * maxForce, wander.y * maxForce);
    }
}
