public class NotDecorator : Decorator {
    public NotDecorator(Behaviour child = null) {
        m_child = child;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_child != null) {
            var r = m_child.Execute(agent, deltaTime);
            switch (r) {
                case eBehaviourResult.SUCCESS: return eBehaviourResult.FAILURE;
                case eBehaviourResult.FAILURE: return eBehaviourResult.SUCCESS;
                default: return eBehaviourResult.RUNNING;
            }
        }

        return eBehaviourResult.FAILURE;
    }
}
