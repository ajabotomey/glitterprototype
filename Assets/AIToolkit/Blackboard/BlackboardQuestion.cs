using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BlackboardQuestion {
    protected int                       m_id;
    protected List<BlackboardExpert>    m_experts;

    public BlackboardQuestion(int id) {
        m_id = id;
        m_experts = new List<BlackboardExpert>();
    }

    ~BlackboardQuestion() {
        m_experts.Clear();
    }

    public int GetQuestionType() {
        return m_id;
    }

    public void AddExpert(BlackboardExpert expert) {
        m_experts.Add(expert);
    }

    public bool Arbitrate(Blackboard blackboard) {
        BlackboardExpert bestExpert = null;
        float bestResponse = 0f;

        // Find the best expert
        foreach (var expert in m_experts) {
            float response = expert.EvaluateResponse(this, blackboard);
            if (response > bestResponse) {
                bestResponse = response;
                bestExpert = expert;
            }
        }

        if (bestExpert != null) {
            bestExpert.Execute(this, blackboard);
        }

        return bestExpert != null;
    }
}
