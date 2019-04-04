using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDecorator : Decorator {
    private System.Delegate methodToExecute; 

    public DelegateDecorator(System.Delegate method) {
        methodToExecute = method;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_child != null) {
            methodToExecute.DynamicInvoke();

            return m_child.Execute(agent, deltaTime);
        }

        return eBehaviourResult.FAILURE;
    }
}
