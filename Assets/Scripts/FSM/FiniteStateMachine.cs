using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour {

    public AIState currentState;

    List<AIState> stateList = new List<AIState>();

	void Start () {
        AIState[] states = GetComponents<AIState> ();
		if(states.Length > 0) {
            foreach (AIState st in states) {
                if (st.disable) {
                    continue;
                }

                st.self = gameObject;
                stateList.Add (st);
                
                if (st.stateName == "NamelessState") {
                    Debug.LogWarning ("A nameless state is detected! Please name your AI state.");
                }
            }

            if (currentState == null) {
                currentState = stateList[0];
            }

			currentState.Enter ();
        }
	}
	
	void Update () {
		if(currentState) {
            currentState.StateUpdate ();
			ChangeState ();
		} else {
            Debug.Log ("Current state is null!");
        }
	}

	void ChangeState () {
		bool needChange = false;
		string nextState = "";

		foreach (ConditionStatePair csp in currentState.events.behaviours.conditionStatePairs) {
			if (csp.ConditionPassed.Invoke ()) {
				needChange = true;
				nextState = csp.decisionState;
				break;
			}
		}

		if (needChange) {
			SetCurrentState (nextState);
		}
	}

	public void SetCurrentState(string name) {
        AIState targetState = GetState (name);
        if (targetState) {
            if(currentState) {
                currentState.Exit ();
            }

            currentState = targetState;
            currentState.Enter ();
        } else {
            Debug.LogError (name + " don't exist!");
        }
    }

    public AIState GetState (string name) {
        AIState targetState = stateList.Find (s => (s.stateName == name));
        return (targetState.stateName != "NamelessState") ? targetState : null;
    }
}
