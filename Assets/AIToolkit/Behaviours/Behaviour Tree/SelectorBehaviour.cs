// If one child suceeeds, the selector is successful

public class SelectorBehaviour : CompositeBehaviour {
    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        int index = m_childIndex;

        m_childIndex = m_children.Count;

        // Reset index if index is at the end of the list
        if (index == m_children.Count) {
            index = 0;
        }

        while (index != m_children.Count) {
            var result = m_children[index].Execute(agent, deltaTime);

            if (result == eBehaviourResult.SUCCESS) {
                return eBehaviourResult.SUCCESS;
            } else if (result == eBehaviourResult.RUNNING) {
                m_childIndex = index;
                return eBehaviourResult.RUNNING;
            }

            index++;
        }

        return eBehaviourResult.FAILURE;
    }
}