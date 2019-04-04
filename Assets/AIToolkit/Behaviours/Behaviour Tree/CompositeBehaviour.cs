using System.Collections.Generic;

public abstract class CompositeBehaviour : Behaviour {
    protected List<Behaviour> m_children;
    protected int m_childIndex;

    public CompositeBehaviour() {
        m_children = new List<Behaviour>();
    }

    public void AddChild(Behaviour child) {
        m_children.Add(child);
    }
}
