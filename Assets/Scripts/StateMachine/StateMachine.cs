using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    
    public IState CurrentState { get; private set; }
    public event Action<IState> stateChanged;
    
    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
        
        stateChanged?.Invoke(state);
    }
    
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
        Debug.Log(nextState.GetType());
	
        
        stateChanged?.Invoke(nextState);
    }
    
    public void Update()
    {
        CurrentState?.Update();
    }
}
