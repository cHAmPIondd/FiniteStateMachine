using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        var stateMachine = new StateMachine();
        var player = new Player();
        var state1 = new TestClass_State(player);
        var state2 = new TestClass_State2(player);
        stateMachine.AddParameter("Status",TestEnum_Status.FishingRod);
        stateMachine.AddState(state1);
        stateMachine.AddState(state2);
        stateMachine.AddConnection(new Connection(state1, state2, (_parameters) =>
        {
            return _parameters.GetValue<TestEnum_Status>("Status") == TestEnum_Status.Hoe;
        }));
        stateMachine.AddConnection(new Connection(state2, state1, (_parameters) =>
        {
            return _parameters.GetValue<TestEnum_Status>("Status") == TestEnum_Status.FishingRod;
        }));
        StateMachineManager.StartMachine(stateMachine, UpdateMode.Update);
    }
}
public enum TestEnum_Status
{
    Hoe,
    FishingRod,
}

public class TestClass_State : BaseState
{
    private Player m_player;
    public TestClass_State(Player _player)
    {
        m_player = _player;
    }
    public override void OnEnter(StateMachineParameters _parameters)
    {
        base.OnEnter(_parameters);
        Debug.Log("EnterState1");
    }
    public override void OnUpdate(StateMachineParameters _parameters,float _deltaTime)
    {
        base.OnUpdate(_parameters,_deltaTime);
        if (Input.GetKeyDown(KeyCode.F1))
            _parameters.SetValue("Status",TestEnum_Status.Hoe);
    }
    public override void OnExit(StateMachineParameters _parameters)
    {
        base.OnExit(_parameters);
        Debug.Log("ExitState1");
    }
}

public class TestClass_State2 : BaseState
{
    private Player m_player;
    public TestClass_State2(Player _player)
    {
        m_player = _player;
    }
    public override void OnEnter(StateMachineParameters _parameters)
    {
        base.OnEnter(_parameters);
        Debug.Log("EnterState2");
    }
    public override void OnUpdate(StateMachineParameters _parameters, float _deltaTime)
    {
        base.OnUpdate(_parameters, _deltaTime);
        if (Input.GetKeyDown(KeyCode.F2))
            _parameters.SetValue("Status", TestEnum_Status.FishingRod);
    }
    public override void OnExit(StateMachineParameters _parameters)
    {
        base.OnExit(_parameters);
        Debug.Log("ExitState2");
    }
}
public class Player
{
    public string holdTool;
}
