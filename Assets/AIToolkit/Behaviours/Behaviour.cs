using UnityEngine;

public enum eBehaviourResult {
    SUCCESS,
    FAILURE,
    RUNNING
}

//public abstract class Behaviour : MonoBehaviour {
public abstract class Behaviour : ScriptableObject {
    public abstract eBehaviourResult Execute(AIAgent agent, float deltaTime);
}
