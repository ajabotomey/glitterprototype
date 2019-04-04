public abstract class Decorator : Behaviour {
    protected Behaviour m_child;

    void SetChild(Behaviour child) { m_child = child; }
}
