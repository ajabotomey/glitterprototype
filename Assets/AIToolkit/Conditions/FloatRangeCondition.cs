public class FloatRangeCondition : Condition {
    private float m_value;
    private float m_min;
    private float m_max;

    public FloatRangeCondition(float value, float min, float max) {
        m_value = value;
        m_min = min;
        m_max = max;
    }

    public override bool Test(AIAgent agent) {
        return (m_min <= m_value) && (m_max >= m_value);
    }
}
