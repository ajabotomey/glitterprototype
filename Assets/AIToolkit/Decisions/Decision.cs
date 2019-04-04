public abstract class Decision {
    public abstract void MakeDecision(AIAgent agent, float deltaTime);
}

public class AttackDecision : Decision {
    public override void MakeDecision(AIAgent agent, float deltaTime) { }
}