using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace FiniteStateMachine
{
    public abstract class BaseState
    {
        public virtual void OnEnter(StateMachineParameters _parameters) { }
        public virtual void OnExit(StateMachineParameters _parameters) { }
        public virtual void OnUpdate(StateMachineParameters _parameters, float _deltaTime) { }
    }
}