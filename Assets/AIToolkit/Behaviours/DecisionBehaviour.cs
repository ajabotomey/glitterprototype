public class DecisionBehaviour : Behaviour {
    protected Decision m_decision;

    public DecisionBehaviour(Decision decision = null) {
        m_decision = decision;
    }

    public void SetDecision(Decision decision) {
        m_decision = decision;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_decision != null) {
            m_decision.MakeDecision(agent, deltaTime);
            return eBehaviourResult.SUCCESS;
        }

        return eBehaviourResult.FAILURE;
    }
}
