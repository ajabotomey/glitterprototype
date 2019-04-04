using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : Behaviour {
    protected State m_currentState;
    protected List<State>       m_states;
    protected List<Transition>  m_transitions;
    protected List<Condition>   m_conditions;

    public FiniteStateMachine() {
        m_currentState = null;
        m_states = new List<State>();
        m_transitions = new List<Transition>();
        m_conditions = new List<Condition>();
    }

    ~FiniteStateMachine() {
        m_states.Clear();
        m_transitions.Clear();
        m_conditions.Clear();
    }

    public void AddState(State state) {
        m_states.Add(state);
    }

    public void AddTransition(Transition transition) {
        m_transitions.Add(transition);
    }

    public void AddCondition(Condition condition) {
        m_conditions.Add(condition);
    }

    public void SetInitialState(State state) {
        if (m_currentState == null) {
            m_currentState = state;
        }
    }

    public State GetCurrentState() {
        return m_currentState;
    }

    public override eBehaviourResult Execute(AIAgent agent, float deltaTime) {
        if (m_currentState != null) {
            // Check for change of state
            if (m_currentState.GetTriggeredTransition(agent) != null) {
                m_currentState.OnExit(agent);
                m_currentState = m_currentState.GetTriggeredTransition(agent).GetTargetState();

                // Reset timer and enter new state
                m_currentState.ResetTimer();
                m_currentState.OnEnter(agent);
            }

            // Accumulate time and update the state
            m_currentState.IncrementTimer(deltaTime);
            m_currentState.UpdateState(agent, deltaTime);

            return eBehaviourResult.SUCCESS;
        }

        return eBehaviourResult.FAILURE;
    }
}
