// All children must succeed for the sequence to be successful

public class SequenceBehaviour : CompositeBehaviour {
    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        int index = m_childIndex;

        m_childIndex = m_children.Count;

        // Reset index if index is at the end of the list
        if (index == m_children.Count) {
            index = 0;
        }

        while (index != m_children.Count) {
            var result = m_children[index].Execute(agent, deltaTime);

            if (result == eBehaviourResult.FAILURE) {
                return eBehaviourResult.FAILURE;
            }
            else if (result == eBehaviourResult.RUNNING) {
                m_childIndex = index;
                return eBehaviourResult.RUNNING;
            }

            index++;
        }

        return eBehaviourResult.SUCCESS;
    }
}
