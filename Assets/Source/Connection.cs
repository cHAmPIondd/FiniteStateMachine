using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class Connection
    {
        private BaseState m_sourceState;
        private BaseState m_targetState;
        private System.Func<StateMachineParameters,bool> m_condition;
        public BaseState SourceState { get { return m_sourceState; } }
        public BaseState TargetState { get { return m_targetState; } }
        public Connection(BaseState _sourceState, BaseState _targetState, System.Func<StateMachineParameters,bool> _condition)
        {
            m_sourceState = _sourceState;
            m_targetState = _targetState;
            m_condition = _condition;
        }
        public bool CheckConditon(StateMachineParameters _parameters)
        {
            return m_condition?.Invoke(_parameters) ?? true;
        }
    }
}