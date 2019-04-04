using UnityEngine;

public class LogDecorator : Decorator {
    protected string m_message;

    public LogDecorator(Behaviour child = null, string msg = "") {
        m_child = child;
        m_message = msg;
    }

    public void Init(Behaviour child = null, string msg = "") {
        m_child = child;
        m_message = msg;
    }

    public void SetMessage(string msg) {
        m_message = msg;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_child != null) {
            Debug.Log(m_message);

            return m_child.Execute(agent, deltaTime);
        }

        return eBehaviourResult.FAILURE;
    }
}
