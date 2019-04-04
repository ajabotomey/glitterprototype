public class FloatGreaterCondition : Condition {
    private float m_value;
    private float m_compare;

    public FloatGreaterCondition(float value, float compare) {
        m_value = value;
        m_compare = compare;
    }

    public FloatGreaterCondition(Ref<float> value, float compare) {
        m_value = value.Value;
        m_compare = compare;
    }

    public override bool Test(AIAgent agent) {
        return m_value > m_compare;
    }
}
