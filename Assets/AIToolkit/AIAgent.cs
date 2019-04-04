using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour {

    protected List<Behaviour> m_behaviours;
    Blackboard m_blackboard;
    [SerializeField] private Rigidbody2D rb;
    public Vector3 movement; // Making public just to test something with the enemy

    public Rigidbody2D rigidbody() {
        return rb;
    }

	// Use this for initialization
	public void Awake () {
        m_behaviours = new List<Behaviour>();
        m_blackboard = new Blackboard();
	}

    public void AddBehaviour(Behaviour behaviour) {
        m_behaviours.Add(behaviour);
    }

    public virtual void UpdateAgent(float deltaTime) {
        foreach (var behaviour in m_behaviours) {
            behaviour.Execute(this, deltaTime);
        }

        // Zero out the velocity
        Vector2 velocity = Vector2.zero;
    }

    public List<Behaviour> GetBehaviours() {
        return m_behaviours;
    }

    public Blackboard GetBlackboard() {
        return m_blackboard;
    }
}
