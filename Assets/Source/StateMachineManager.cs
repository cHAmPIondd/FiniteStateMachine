using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace FiniteStateMachine
{
    public enum UpdateMode
    {
        Update,
        LateUpdate,
        FixedUpdate,
    }
    public class StateMachineManager : MonoBehaviour
    {
        private static List<StateMachine> m_updateStateMachines = new List<StateMachine>();
        private static List<StateMachine> m_lateUpdateStateMachine = new List<StateMachine>();
        private static List<StateMachine> m_fixedUpdateStateMachine = new List<StateMachine>();
        private static StateMachineManager m_instance;
        /// <summary>
        /// Check whether this manager mono instance exists. If not, create one
        /// </summary>
        public static void CheckInstanceExist()
        {
            if (m_instance == null)
            {
                var go = new GameObject("[StateMachineManager]");
                GameObject.DontDestroyOnLoad(go);
                m_instance = go.AddComponent<StateMachineManager>();
            }
        }
        /// <summary>
        /// Start a machine, and the update mode determines its update timing.
        /// </summary>
        /// <param name="_machine"></param>
        /// <param name="_updateMode"></param>
        public static void StartMachine(StateMachine _machine, UpdateMode _updateMode = UpdateMode.Update)
        {
            Assert.IsNotNull(_machine);
            switch (_updateMode)
            {
                case UpdateMode.Update:
                    m_updateStateMachines.Add(_machine);
                    break;
                case UpdateMode.LateUpdate:
                    m_lateUpdateStateMachine.Add(_machine);
                    break;
                case UpdateMode.FixedUpdate:
                    m_fixedUpdateStateMachine.Add(_machine);
                    break;
            }
            _machine.Start();
            CheckInstanceExist();
        }
        /// <summary>
        /// Stop a running machine
        /// </summary>
        /// <param name="_machine"></param>
        public static void StopMachine(StateMachine _machine)
        {
            Assert.IsNotNull(_machine);
            if (m_updateStateMachines.Remove(_machine)
             || m_lateUpdateStateMachine.Remove(_machine)
             || m_fixedUpdateStateMachine.Remove(_machine))
            {
                _machine.Stop();
            }
            CheckInstanceExist();
        }
        private void Update()
        {
            foreach (var machine in m_updateStateMachines)
                machine.Update(Time.deltaTime);
        }
        private void FixedUpdate()
        {
            foreach (var machine in m_fixedUpdateStateMachine)
                machine.Update(Time.deltaTime);
        }
        private void LateUpdate()
        {
            foreach (var machine in m_lateUpdateStateMachine)
                machine.Update(Time.deltaTime);
        }
    }
}