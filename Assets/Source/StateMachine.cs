using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        private StateMachineParameters m_parameters = new StateMachineParameters();
        private List<BaseState> m_states = new List<BaseState>();
        private List<Connection> m_connections = new List<Connection>();
        private List<Connection> m_currentActiveConnections = new List<Connection>();
        private BaseState m_currentState;
        /// <summary>
        /// Start the machine, and the first added state will be the first executed state
        /// </summary>
        public void Start()
        {
            Assert.IsTrue(m_states.Count != 0, "Cannot start machine when the states count is 0");
            TransitionToState(m_states.FirstOrDefault());
        }
        /// <summary>
        /// Stop the machine,state will be switch to null
        /// </summary>
        public void Stop()
        {
            TransitionToState(null);
        }
        public void Update(float _deltaTime)
        {
            foreach (var connection in m_currentActiveConnections)
            {
                if (connection.CheckConditon(m_parameters))
                {
                    TransitionToState(connection.TargetState);
                    break;
                }
            }
            m_currentState?.OnUpdate(m_parameters,_deltaTime);
        }
        public void AddState(BaseState _state)
        {
            m_states.Add(_state);
        }
        /// <summary>
        /// Add connection that detemine state how to switch states
        /// </summary>
        /// <param name="_connection"></param>
        public void AddConnection(Connection _connection)
        {
            m_connections.Add(_connection);
            RefreshCurrentActiveConnections();
        }
        /// <summary>
        /// Remove connection that detemine state how to switch states
        /// </summary>
        /// <param name="_connection"></param>
        public void RemoveConnection(Connection _connection)
        {
            if(m_connections.Remove(_connection))
                RefreshCurrentActiveConnections();
        }
        public void AddParameter<T>(string _key,T _initalValue)
        {
            m_parameters.AddParameter(_key, _initalValue);
        }
        public void RmoveParameter(string _key)
        {
            m_parameters.RemoveParameter(_key);
        }
        /// <summary>
        /// Usually, you only need to use connection to switch the state, but sometimes you need to use this when you need to force the state to switch
        /// </summary>
        /// <param name="_state"></param>
        public void ForceTransitionToState(BaseState _state)
        {
            TransitionToState(_state);
        }
        private void TransitionToState(BaseState _newState)
        {
            m_currentState?.OnExit(m_parameters);
            m_currentState = _newState;
            RefreshCurrentActiveConnections();
            _newState?.OnEnter(m_parameters);
        }
        /// <summary>
        /// Refresh the connection list, it depends on the current state 
        /// </summary>
        private void RefreshCurrentActiveConnections()
        {
            m_currentActiveConnections = m_connections.FindAll(x => x.SourceState == m_currentState);
        }
    }
}
