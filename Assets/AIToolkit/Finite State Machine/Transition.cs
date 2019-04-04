using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition {
    private State       m_target;
    private Condition   m_condition;

    public Transition(State target, Condition condition) {
        m_target = target;
        m_condition = condition;
    }

    public State GetTargetState() {
        return m_target;
    }

    public bool HasTriggered(AIAgent agent) {
        return m_condition.Test(agent);
    }
}
