public class NotCondition : Condition {
    private Condition m_condition;

    public NotCondition(Condition condition) {
        m_condition = condition;
    }

    public override bool Test(AIAgent agent) {
        return !m_condition.Test(agent);
    }
}
