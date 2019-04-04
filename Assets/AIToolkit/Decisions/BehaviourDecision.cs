public class BehaviourDecision : Decision {
    protected Behaviour m_behaviour;

    public BehaviourDecision(Behaviour behaviour = null) {
        m_behaviour = behaviour;
    }

    public void SetBehaviour(Behaviour behaviour) {
        m_behaviour = behaviour;
    }

    public override void MakeDecision(AIAgent agent, float deltaTime) {
        if (m_behaviour != null) {
            m_behaviour.Execute(agent, deltaTime);
        }
    }
}
