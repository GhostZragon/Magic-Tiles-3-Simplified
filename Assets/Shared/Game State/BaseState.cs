using System;
using UnityEngine;

/// <summary>
/// Abstract base class for game states, providing a template for state management.
/// </summary>
[Serializable]
public abstract class BaseState : IState
{

    protected GameContext GameContext;
    public virtual void Initialize(GameContext GameContext)
    {
        this.GameContext = GameContext;
    }

    /// <summary>
    /// Reference to the GameStateManager instance.
    /// </summary>
    private readonly GameStateManager stateManager = GameStateManager.Instance;

    /// <summary>
    /// Prepares the state by calling CatchRef, Register, and AfterPrepareState methods.
    /// </summary>
    public virtual void Enter()
    {
        Register();
        AfterPrepareState();
    }

    /// <summary>
    /// Destroys the state by calling the UnRegister and AfterDestroyState methods.
    /// </summary>
    public virtual void Exit()
    {
        UnRegister();
        AfterDestroyState();
    }
    
    /// <summary>
    /// This method call every frame
    /// </summary>
    public virtual void Update()
    {
    }

    /// <summary>
    /// Method to be called after preparing the state. Can be overridden by derived classes.
    /// </summary>
    protected virtual void AfterPrepareState()
    {
    }

    /// <summary>
    /// Method to be called after destroying the state. Can be overridden by derived classes.
    /// </summary>
    protected virtual void AfterDestroyState()
    {
    }

    /// <summary>
    /// Registers the state. This method can be overridden by derived classes to provide specific registration logic.
    /// </summary>
    protected virtual void Register()
    {
    }

    /// <summary>
    /// Unregisters the state. This method can be overridden by derived classes to provide specific unregistration logic.
    /// </summary>
    protected virtual void UnRegister()
    {
    }

    /// <summary>
    /// Changes the current state to a new state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    protected void ChangeState<T>() where T : BaseState
    {
        stateManager.ChangeState<T>();
    }
}