using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {
    protected float             m_timer;
    protected List<Transition>  m_transitions;

    public State() {
        m_timer = 0.0f;
        m_transitions = new List<Transition>();
    }

    ~State() {
        m_transitions.Clear();
    }

    public abstract void UpdateState(AIAgent agent, float deltaTime);

    public virtual void OnEnter(AIAgent agent) { }
    public virtual void OnExit(AIAgent agent) { }

    public float GetTimer() {
        return m_timer;
    }

    public Ref<float> GetTimerPtr() {
        Ref<float> x;

        x = new Ref<float>(() => m_timer, z => { m_timer = z; });

        return x;
    }

    public void ResetTimer() {
        m_timer = 0f;
    }

    public void IncrementTimer(float deltaTime) {
        m_timer += deltaTime;
    }

    public void AddTransition(Transition transition) {
        m_transitions.Add(transition);
    }

    public Transition GetTriggeredTransition(AIAgent agent) {
        foreach (var transition in m_transitions) {
            if (transition.HasTriggered(agent)) {
                return transition;
            }
        }

        return null;
    }
}

public sealed class Ref<T> {
    private readonly Func<T> getter;
    private readonly Action<T> setter;
    public Ref(Func<T> getter, Action<T> setter) {
        this.getter = getter;
        this.setter = setter;
    }
    public T Value { get { return getter(); } set { setter(value); } }
}
