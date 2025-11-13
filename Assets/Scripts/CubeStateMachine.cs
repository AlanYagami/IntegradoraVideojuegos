using System;
using UnityEngine;

public class CubeStateMachine : MonoBehaviour, IStateMachine
{
    public IState CurrentState { get; set; }

    private void Start() => ChangeState(new IdleState(this));

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
    
    void Update() => CurrentState?.Tick(Time.deltaTime);
}

public struct IdleState : IState
{
    public CubeStateMachine StateMachine { get; set; }

    public IdleState(CubeStateMachine stateMachine) => StateMachine = stateMachine;

    public void Enter() => Debug.Log("Enter Idle State");

    public void Tick(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Space)) StateMachine.ChangeState(new RotatingState(StateMachine));
    }

    public void Exit() => Debug.Log("Exit Idle State");
}

public struct RotatingState : IState
{
    public CubeStateMachine StateMachine { get; set; }

    public RotatingState(CubeStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public void Enter() => Debug.Log("Enter Rotating State");

    public void Tick(float deltaTime)
    {
        StateMachine.transform.Rotate(0f, 360f * deltaTime, 0f);
        if (Input.GetKeyDown(KeyCode.Space)) StateMachine.ChangeState(new IdleState(StateMachine));
    }

    public void Exit() => Debug.Log("Exit Rotating State");
}