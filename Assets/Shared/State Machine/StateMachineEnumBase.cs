using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineEnumBase<TState> : MonoBehaviour where TState : IStateExtend
{
    protected Dictionary<string, TState> states = new();
    protected TState currentState;
    protected string currentStateName;

    protected void AddState(Enum enumState, TState instance)
    {
        AddState(enumState.ToString(), instance);
    }

    protected void AddState(string typeName, TState instance)
    {
        states.Add(typeName, instance);
        InjectReferences(instance);
    }

    public virtual void ChangeState(string stateName, object data = null)
    {
        Debug.Log($"Change State {stateName}", gameObject);
        currentStateName = stateName;

        states.TryGetValue(stateName, out var instance);

        currentState?.Exit();
        currentState = instance;
        currentState?.Enter(data);
    }

    public virtual void ChangeState(Enum enumState, object data = null)
    {
        ChangeState(enumState.ToString(), data);
    }

    protected abstract void InjectReferences(TState TState);

    protected virtual void Update()
    {
        currentState?.Update();
    }
}