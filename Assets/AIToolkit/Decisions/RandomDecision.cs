using System.Collections.Generic;
using UnityEngine;

public class RandomDecision : Decision {
    protected List<Decision> m_decisions;

    public void AddDecision(Decision decision) {
        m_decisions.Add(decision);
    }

    public override void MakeDecision(AIAgent agent, float deltaTime) {
        if (m_decisions.Count != 0) {
            m_decisions[Random.Range(0, m_decisions.Count)].MakeDecision(agent, deltaTime);
        }
    }
}
