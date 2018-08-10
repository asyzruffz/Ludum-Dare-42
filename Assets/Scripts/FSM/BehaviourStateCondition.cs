using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BehaviourStateCondition {
    public List<ConditionStatePair> conditionStatePairs = new List<ConditionStatePair> ();
}

[Serializable]
public class Condition : SerializableCallback<bool> { }

[Serializable]
public struct ConditionStatePair {
    public Condition ConditionPassed;
    public string decisionState;
}