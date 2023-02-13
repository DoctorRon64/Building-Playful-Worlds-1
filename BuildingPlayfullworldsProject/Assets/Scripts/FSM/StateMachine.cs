using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateMachine 
{
    private Dictionary<System.Type, State> stateDictionary = new Dictionary<System.Type, State>();
    private State activeState;

    public Guard Controller;

    // Start is called before the first frame update
    public StateMachine(Guard Controller, params State[] states)
    {
        this.Controller = Controller;
        foreach(State state in states)
        {
            Debug.Log(state.GetType().Name);
            state.Initalize(this);
            stateDictionary.Add(state.GetType(), state); 
        }
    }

    //Overloading
    public void DoSomething()
    {
    }

    public void DoSomething(int value)
    {

    }
    public void DoSomething(int value, string doSomething)
    {

    }

    // Update is called once per frame
    public void OnUpdate()
    {
        activeState?.OnUpdate();
    }

    public void SwitchState(System.Type stateName)
    {
        activeState?.OnExit();
        if (stateDictionary.ContainsKey(stateName))
        {
            activeState = stateDictionary[stateName];
        }
        activeState?.OnEnter();
    }
}
