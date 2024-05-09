using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    
    public IState currentState { get; private set; }
    public event Action<IState> StateChanged;
    
    public void Initialize(IState state)
    {
        currentState = state;
        state.Enter();
        
        StateChanged?.Invoke(state);
    }
    
    public void TransitionTo(IState nextState)
    {
        currentState.Exit();
        currentState = nextState;
        nextState.Enter();
	
        
        StateChanged?.Invoke(nextState);
    }
    
    public void Update()
    {
        currentState?.Update();
    }
}
