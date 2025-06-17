using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateWithSubStates : BaseState
{
    protected IState currentSubState;
    protected readonly Dictionary<Type, IState> subStates = new();

    protected void RegisterSubState<T>(T state) where T : BaseState
    {
        Type stateType = typeof(T);

        subStates[stateType] = state;
        state.Initialize();
        Debug.Log($" Registered sub-state: {stateType.Name} ");
    }

    public void ChangeSubState<T>() where T : IState
    {
        if (currentSubState != null)
        {
            currentSubState.Exit();
        }
        Type stateType = typeof(T);

        currentSubState = subStates[stateType];
        currentSubState.Enter();
        LogCurrentState();
    }

    public override void Exit()
    {
        base.Exit();
        if(currentSubState != null)
            currentSubState.Exit();
    }

    protected void LogCurrentState()
    {
        string currentStateName = currentSubState?.GetType().Name ?? "No active sub-state";
        Debug.Log($"[{currentSubState}] Current sub-state: {currentStateName}");
    }
}

