public class LimitDecorator : Decorator {
    protected int m_count;

    public LimitDecorator(Behaviour child = null, int limit = 1) {
        m_child = child;
        m_count = limit;
    }

    public void SetLimit(int limit) {
        m_count = limit;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_child != null) {
            --m_count;

            return m_child.Execute(agent, deltaTime);
        }

        return eBehaviourResult.FAILURE;
    }
}
