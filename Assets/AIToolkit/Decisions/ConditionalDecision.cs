public class ConditionalDecision : Decision {
    protected Condition m_condition;
    protected Decision m_trueBranch;
    protected Decision m_falseBranch;

    public ConditionalDecision() {
        m_condition = null;
        m_trueBranch = null;
        m_falseBranch = null;
    }

    public ConditionalDecision(Condition condition, Decision trueBranch, Decision falseBranch) {
        m_condition = condition;
        m_trueBranch = trueBranch;
        m_falseBranch = falseBranch;
    }

    public void SetCondition(Condition condition) {
        m_condition = condition;
    }

    public void SetTrueBranch(Decision decision) {
        m_trueBranch = decision;
    }

    public void SetFalseBranch(Decision decision) {
        m_falseBranch = decision;
    }

    public override void MakeDecision(AIAgent agent, float deltaTime) {
        if (m_condition != null && m_trueBranch != null && m_falseBranch != null) {
            if (m_condition.Test(agent)) {
                m_trueBranch.MakeDecision(agent, deltaTime);
            } else {
                m_falseBranch.MakeDecision(agent, deltaTime);
            }
        }
    }
}
