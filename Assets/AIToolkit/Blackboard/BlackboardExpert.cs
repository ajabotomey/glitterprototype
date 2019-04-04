using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlackboardExpert {
    public abstract float EvaluateResponse(BlackboardQuestion question, Blackboard blackboard);
    public abstract void Execute(BlackboardQuestion question, Blackboard blackboard);
}
