using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(FiniteStateMachine))]
public class AIState : MonoBehaviour {

    public string stateName = "NamelessState";
	public StateEvents events;

    public bool disable; // for debugging

    [HideInInspector]
    public GameObject self;
    protected FiniteStateMachine machine {
        get {
            if(self && self.GetComponent<FiniteStateMachine>()) {
                return self.GetComponent<FiniteStateMachine> ();
            } else {
                return null;
            }
        }
    }
    
    public virtual void StateUpdate () {
        if (events.onUpdate != null) {
            events.onUpdate.Invoke ();
        }
    }

    public virtual void CheckForStateExit () {
        
    }

    public virtual void Enter () {
        //Debug.Log (self.name + " enters " + stateName + " state!");
        if (events.onEnter != null) {
            events.onEnter.Invoke ();
        }
    }

    public virtual void Exit () {
        //Debug.Log (self.name + " exits " + stateName + " state!");
        if (events.onExit != null) {
            events.onExit.Invoke ();
        }
    }
}

[System.Serializable]
public struct StateEvents {
    public UnityEvent onEnter;
	public BehaviourStateCondition behaviours;
	public UnityEvent onUpdate;
	public UnityEvent onExit;
}